


namespace PersonalFinanceApp.Data.Services;

public interface IUserInfoService
{
    public Task<UserResult?> GetUserById(Guid userId);
    public Task<string?> GetUserItemById(Guid userId, string key);

}