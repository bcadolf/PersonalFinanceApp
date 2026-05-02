

using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Data.Models;

public abstract class BaseModel
{
    [Key]
    public Guid Id { get; set; } 

    [Required]
    public DateTime lastUpdated { get; set; } = DateTime.UtcNow;

    [Required]
    public bool isDeleted { get; set; } = false;
}