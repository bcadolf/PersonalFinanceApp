

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalFinanceApp.Data.Models;

public class FinanceCategory : BaseModel
{

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public Guid UserId { get; set; }

    public List<BudgetItem> BudgetItems { get; set;} = new();

}