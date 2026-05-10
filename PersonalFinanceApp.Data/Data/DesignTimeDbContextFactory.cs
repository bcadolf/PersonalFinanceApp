using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PersonalFinanceApp.Data.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // This is only used by the 'dotnet ef' command line tool
        // It does not affect your actual mobile app database path
        optionsBuilder.UseSqlite("Data Source=migration_only.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}
