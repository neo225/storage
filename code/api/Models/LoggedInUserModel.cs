namespace I2R.Storage.Api.Models;

public class LoggedInUserModel
{
    public LoggedInUserModel() { }

    public LoggedInUserModel(ClaimsPrincipal user) {
        Username = user.FindFirstValue(AppClaims.USERNAME);
        Id = user.FindFirstValue(AppClaims.USER_ID).AsGuid();
        Role = UserRole.FromString(user.FindFirstValue(AppClaims.USER_ROLE));
    }

    public string Username { get; set; }
    public Guid Id { get; set; }
    public EUserRole Role { get; set; }

    public class Public
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public static Public ForThePeople(HttpContext httpContext) {
        return new Public() {
            Id = httpContext.User.FindFirstValue(AppClaims.USER_ID),
            Username = httpContext.User.FindFirstValue(AppClaims.USERNAME),
            Role = httpContext.User.FindFirstValue(AppClaims.USER_ROLE)
        };
    }
}