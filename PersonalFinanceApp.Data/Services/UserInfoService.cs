

using PersonalFinanceApp.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace PersonalFinanceApp.Data.Services;

public record UserResult(string? FirstName, string? LastName, string? UserName, string? Email);

public class UserInfoService : IUserInfoService
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

    public async Task<string?> GetUserItemById(Guid userId, string key)
    {
        return await _db.Users
            .Where(u => u.Id == userId)
            .Select<string?>(key)
            .FirstOrDefaultAsync();
    }


}