


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data.Models.Auth;
using PersonalFinanceApp.Data.Models;

namespace PersonalFinanceApp.Data.Data;

public class AppDbContext : IdentityDbContext<UserProfile, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<BudgetItem> BudgetItems { get; set; }
    public DbSet<FinanceCategory> FinanceCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}