

namespace PersonalFinanceApp.Data.Models;

public class Budget : BaseModel
{
    public string BudgetName { get; set; } = string.Empty;

    public Guid UserProfileId { get; set; }

    public List<BudgetItem> BudgetItems { get; set; } = new();
    public bool IsCurrent { get; set; } = false;

}