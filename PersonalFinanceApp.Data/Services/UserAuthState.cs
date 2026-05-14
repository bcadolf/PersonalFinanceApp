

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.Storage;

namespace PersonalFinanceApp.Data.Services;

public class UserAuthState : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userId = await SecureStorage.Default.GetAsync("user_id");
            var username = await SecureStorage.Default.GetAsync("user_name");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
            {
                return new AuthenticationState(_anonymous);
            }  
                
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            }, "CustomAuth");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        } catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task Login(string userId, string username)
    {
        await SecureStorage.Default.SetAsync("user_id", userId);
        await SecureStorage.Default.SetAsync("user_name", username);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username)
        }, "CustonAuth");

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public async Task Logout()
    {
        SecureStorage.Default.Remove("user_id");
        SecureStorage.Default.Remove("user_name");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
