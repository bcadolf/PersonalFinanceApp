

namespace PersonalFinanceApp.Data.Models;

public class FinanceAccount : BaseModel
{
    public string AccountName { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0.00m;
    public Guid UserProfileId { get; set; }
    public string FinanceAccountBank { get; set; } = string.Empty;
}