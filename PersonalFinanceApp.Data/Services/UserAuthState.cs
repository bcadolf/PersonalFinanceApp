

using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace PersonalFinanceApp.Data.Services;

public class UserAuthState : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private ClaimsPrincipal? _currentUser;

    public string CurrentUserId => 
        _currentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public Guid CurrentUserGuid =>
        Guid.TryParse(CurrentUserId, out var guid) ? guid : Guid.Empty;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_currentUser != null)
        {
            return new AuthenticationState(_currentUser);
        }

        try
        {
            string? userId = null;
            string? username = null;
            // added to fix hidden bug of crash if Secure storage accessed during page initalize
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                userId = await SecureStorage.Default.GetAsync("user_id");
                username = await SecureStorage.Default.GetAsync("user_name");
            });

            

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
            {
                _currentUser = _anonymous;
                return new AuthenticationState(_anonymous);
            }  
                
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            }, "CustomAuth");

            _currentUser = new ClaimsPrincipal(identity);
            return new AuthenticationState(new ClaimsPrincipal(identity));
        } catch (Exception ex)
        {
            Debug.WriteLine($"Auth Read Error: {ex.Message}");
            _currentUser = _anonymous;
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task Login(string userId, string username)
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await SecureStorage.Default.SetAsync("user_id", userId);
                await SecureStorage.Default.SetAsync("user_name", username);
            });

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            }, "CustomAuth");

            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        } catch (Exception ex)
        {
            Debug.WriteLine($"Login Storage Error: {ex.Message}");
            throw;
        }
        

        
    }

    public async Task Logout()
    {

        try
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                SecureStorage.Default.Remove("user_id");
                SecureStorage.Default.Remove("user_name");
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Logout Storage Error: {ex.Message}");
        }

        _currentUser = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
   
    }
}
