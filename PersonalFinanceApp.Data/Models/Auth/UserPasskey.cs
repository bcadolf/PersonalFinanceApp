

using System.ComponentModel.DataAnnotations;
using PersonalFinanceApp.Data.Models;


namespace PersonalFinanceApp.Data.Models.Auth;

public class UserPasskey : BaseModel
{
    public Guid UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; } = null!;

    [Required]
    public byte[] PublicKey { get; set; } = null!;
    [Required]
    public byte[] UserHandle { get; set; } = null!;
    public int SignatureCounter { get; set; } 
    [Required]
    public string CredentialId { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(75)]
    public string DeviceName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}