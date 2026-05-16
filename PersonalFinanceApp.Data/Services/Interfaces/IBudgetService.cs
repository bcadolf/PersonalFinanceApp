
using PersonalFinanceApp.Data.Models;

namespace PersonalFinanceApp.Data.Services;

public interface IBudgetService
{
    public Task<Budget?> GetCurrentBudgetById(Guid userId);
    public Task SetCurrentBudget(Guid userId, Guid budgetId);
    public Task<List<Budget>> GetBudgetsById(Guid userId);
    public Task<Budget> CreateNewBudget(Guid userId, string name);
    public Task AddItemToBudget(Guid budgetId, BudgetItem item);
}