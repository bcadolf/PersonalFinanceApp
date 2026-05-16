

namespace PersonalFinanceApp.Data.Services;

public interface ILoginService
{
    Task<bool> UsernameFound(string username);
    Task CreateUser(string username, string password, string firstname, string lastname, string email);
    Task<string> ValidatePassword(string username, string password);

}