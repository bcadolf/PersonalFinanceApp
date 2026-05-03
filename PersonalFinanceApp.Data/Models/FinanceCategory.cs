

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalFinanceApp.Data.Models;

public class FinanceCategory : BaseModel
{

    [Required(ErrorMessage = "Please enter a name for the category.")]
    [MaxLength(100, ErrorMessage = "The category name cannot exceed 100 characters.")]
    public string CategoryName { get; set; } = string.Empty;

    public Guid UserProfileId { get; set; }

    public List<BudgetItem> BudgetItems { get; set;} = new();

}