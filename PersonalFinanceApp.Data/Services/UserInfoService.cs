

using PersonalFinanceApp.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceApp.Data.Services;

public record UserResult(string? FirstName, string? LastName, string? UserName, string? Email);

public class UserInfoService
{
    private readonly AppDbContext _db;

    public UserInfoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserResult?> GetUserById(Guid userId)
    {
        return await _db.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserResult(
                u.FirstName,
                u.LastName,
                u.UserName,
                u.Email
            ))
            .FirstOrDefaultAsync();
    }


}