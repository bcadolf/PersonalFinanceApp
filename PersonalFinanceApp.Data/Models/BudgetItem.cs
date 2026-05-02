

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceApp.Data.Models;
public class BudgetItem : BaseModel
{

    [Required]
    [MaxLength(100)]
    public string ItemName { get; set; } = string.Empty;

    [Required]
    public decimal Amount { get; set; } = 0;


    [ForeignKey("FinanceCategoryId")]
    public Guid FinanceCategoryId { get; set; }

    public FinanceCategory FinanceCategory { get; set; } = null!;
}