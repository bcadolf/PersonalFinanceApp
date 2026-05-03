using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinanceApp.Data.Data;
using PersonalFinanceApp.Data.Models.Auth;

namespace PersonalFinanceApp.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "personalfinanceapp.db");

		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Filename={dbPath}"));

		builder.Services.AddIdentityCore<UserProfile>()
			.AddRoles<IdentityRole<Guid>>()
			.AddEntityFrameworkStores<AppDbContext>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();

		//migrate database on startup
		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			db.Database.Migrate();
		}

		return app;
	}
}
