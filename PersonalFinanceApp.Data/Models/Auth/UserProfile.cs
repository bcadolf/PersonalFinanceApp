

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalFinanceApp.Data.Models.Auth;

public class UserProfile : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 

    // Finacial data lists
    public List<FinanceCategory> FinanceCategories { get; set; } = new();
    public List<Budget> Budgets { get; set; } = new();


    // OTP or passkeys decide later to add or not. 
    public List<UserPasskey> UserPasskeys { get; set; } = new();
    
}