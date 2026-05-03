

namespace PersonalFinanceApp.Data.Models;

public class Transaction : BaseModel
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0.00m;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public bool IsPending { get; set; } = true;

    public Guid FinanceAccountId { get; set; }
    public Guid FinanceCategoryId { get; set; }
    public Guid UserProfileId { get; set; }

    public FinanceAccount? FinanceAccount { get; set; }
    public FinanceCategory? FinanceCategory { get; set; }
}