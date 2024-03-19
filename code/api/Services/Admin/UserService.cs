using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Quality.Storage.Api.Services.Admin;

public class UserService(AppDatabase database, ILogger<UserService> logger)
{
	public bool CanCreateAccount(string username) {
        if (username.IsNullOrWhiteSpace()) {
            return false;
        }

        var normalisedUsername = username.Trim();
        return database.Users.All(c => c.Username != normalisedUsername);
    }

    public async Task LogInUserAsync(HttpContext httpContext, IEnumerable<Claim> claims) {
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var authenticationProperties = new AuthenticationProperties {
            AllowRefresh = true,
            IssuedUtc = DateTimeOffset.UtcNow,
        };

        await httpContext.SignInAsync(principal, authenticationProperties);
        logger.LogInformation("Logged in user {userId}", principal.FindFirstValue(AppClaims.USER_ID));
    }

    public async Task LogOutUserAsync(HttpContext httpContext, CancellationToken cancellationToken = default) {
        await httpContext.SignOutAsync();
        logger.LogInformation("Logged out user {userId}", httpContext.User.FindFirstValue(AppClaims.USER_ID));
    }

    public async Task MarkUserAsDeletedAsync(Guid userId, Guid actorId) {
        var user = database.Users.FirstOrDefault(c => c.Id == userId);
        if (user == default) {
            logger.LogInformation("Tried to delete unknown user {userId}", userId);
            return;
        }

        user.SetDeleted(actorId);
        await database.SaveChangesAsync();
    }
}