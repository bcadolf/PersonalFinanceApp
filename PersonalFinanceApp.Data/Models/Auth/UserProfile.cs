

using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Data.Models.Auth;

public class UserProfile : BaseModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    public string? PasswordHash { get; set; }

    // OTP or passkeys decide later to add or not. 
}