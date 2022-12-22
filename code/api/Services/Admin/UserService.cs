using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace I2R.Storage.Api.Services.Admin;

public class UserService
{
    private readonly AppDatabase _database;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDatabase database, ILogger<UserService> logger) {
        _database = database;
        _logger = logger;
    }

    public bool CanCreateAccount(string username) {
        if (username.IsNullOrWhiteSpace()) {
            return false;
        }

        var normalisedUsername = username.Trim();
        return _database.Users.All(c => c.Username != normalisedUsername);
    }

    public async Task LogInUserAsync(HttpContext httpContext, IEnumerable<Claim> claims) {
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var authenticationProperties = new AuthenticationProperties {
            AllowRefresh = true,
            IssuedUtc = DateTimeOffset.UtcNow,
        };

        await httpContext.SignInAsync(principal, authenticationProperties);
        _logger.LogInformation("Logged in user {userId}", principal.FindFirstValue(AppClaims.USER_ID));
    }

    public async Task LogOutUserAsync(HttpContext httpContext, CancellationToken cancellationToken = default) {
        await httpContext.SignOutAsync();
        _logger.LogInformation("Logged out user {userId}", httpContext.User.FindFirstValue(AppClaims.USER_ID));
    }

    public async Task MarkUserAsDeletedAsync(Guid userId, Guid actorId) {
        var user = _database.Users.FirstOrDefault(c => c.Id == userId);
        if (user == default) {
            _logger.LogInformation("Tried to delete unknown user {userId}", userId);
            return;
        }

        user.SetDeleted(actorId);
        await _database.SaveChangesAsync();
    }
}