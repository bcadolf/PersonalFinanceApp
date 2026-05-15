using System.Diagnostics;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinanceApp.Data.Data;
using PersonalFinanceApp.Data.Models.Auth;
using PersonalFinanceApp.Data.Services;

namespace PersonalFinanceApp.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		
		var builder = MauiApp.CreateBuilder();

		SQLitePCL.Batteries_V2.Init();

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "personalfinanceapp.db");

		var dbKey = DatabaseSecurity.GetKeySync();

		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath};Password={dbKey}"));
		

		builder.Services.AddIdentityCore<UserProfile>()
			.AddRoles<IdentityRole<Guid>>()
			.AddEntityFrameworkStores<AppDbContext>();
		builder.Services.AddScoped<LoginService>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		// Auth services
		builder.Services.AddAuthorizationCore();
		builder.Services.AddScoped<UserAuthState>();
		builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<UserAuthState>());

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();

		//migrate database on startup
		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			DbInitializer.Initialize(db);
		}

		return app;
	}
}
