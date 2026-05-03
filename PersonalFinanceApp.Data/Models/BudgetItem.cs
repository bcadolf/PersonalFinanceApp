

using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Data.Models;
public class BudgetItem : BaseModel
{

    [Required(ErrorMessage = "Please enter a name for the budget item.")]
    [MaxLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
    public string ItemName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a valid amount.")]
    public decimal Amount { get; set; } = 0.00m;


    
    public Guid FinanceCategoryId { get; set; }
    
    public Guid UserProfileId { get; set; }

    public FinanceCategory? FinanceCategory { get; set; }

}