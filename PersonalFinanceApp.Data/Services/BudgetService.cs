

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data.Data;
using PersonalFinanceApp.Data.Models;

namespace PersonalFinanceApp.Data.Services;

public class BudgetService : IBudgetService
{
    private readonly AppDbContext _db;

    public BudgetService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Budget?> GetCurrentBudgetById(Guid userId)
    {
        return await _db.Budgets
            .Where(b => b.UserProfileId == userId && b.IsCurrent == true)
            .Include(b => b.BudgetItems)
            .FirstOrDefaultAsync();
    }

    public async Task SetCurrentBudget(Guid userId, Guid budgetId)
    {
        await _db.Budgets
            .Where(b => b.UserProfileId == userId && b.IsCurrent == true)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.IsCurrent, false)
                .SetProperty(b => b.lastUpdated, DateTime.UtcNow));

        await _db.Budgets
            .Where(b => b.Id == budgetId && b.UserProfileId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.IsCurrent,true)
                .SetProperty(b => b.lastUpdated, DateTime.UtcNow));
    }

    public Task<List<Budget>> GetBudgetsById(Guid userId)
    {
        return _db.Budgets
            .Where(b => b.UserProfileId == userId)
            .Include(b => b.BudgetItems)
            .ToListAsync();
    }

    public async Task<Budget> CreateNewBudget(Guid userId, string name)
    {
        var newBudget = new Budget()
        {
            UserProfileId = userId,
            BudgetName = name
        };

        _db.Budgets.Add(newBudget);
        await _db.SaveChangesAsync();

        return newBudget;
    }

    public async Task AddItemToBudget(Guid budgetId, BudgetItem item)
    {
        item.BudgetId = budgetId;
        
        _db.BudgetItems.Add(item);

        await _db.SaveChangesAsync();
    }


}