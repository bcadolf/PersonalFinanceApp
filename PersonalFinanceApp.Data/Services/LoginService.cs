

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data.Data;
using PersonalFinanceApp.Data.Models.Auth;

namespace PersonalFinanceApp.Data.Services;

public class LoginService
{
    private readonly AppDbContext _db;

    private readonly PasswordHasher<UserProfile> _passwordHasher = new();

    public LoginService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> UsernameFound(string username)
    {
        return await _db.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task CreateUser(string username, string password, string firstname, string lastname, string email)
    {
        var user = new UserProfile
        {
            UserName = username,
            FirstName = firstname,
            LastName = lastname,
            Email = email
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<string> ValidatePassword(string username, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        string userId = string.Empty;

        if (user == null || string.IsNullOrEmpty(user.PasswordHash))
        {
            return string.Empty;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Success)
        {
            return userId = user.Id.ToString();
        }

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _db.SaveChangesAsync();

            return user.Id.ToString();
        }
        
        return string.Empty;
    }
}