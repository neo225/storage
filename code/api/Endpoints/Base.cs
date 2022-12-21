using System.Security.Claims;

namespace I2R.Storage.Api.Endpoints;

[ApiController]
[Authorize]
public class Base : ControllerBase
{
    public class LoggedInUserModel
    {
        public string Username { get; set; }
        public Guid Id { get; set; }
        public EUserRole Role { get; set; }

        public class Public
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
        }

        public Public ForThePeople(HttpContext httpContext) {
            return new Public() {
                Id = httpContext.User.FindFirstValue(AppClaims.USER_ID),
                Username = httpContext.User.FindFirstValue(AppClaims.USERNAME),
                Role = httpContext.User.FindFirstValue(AppClaims.USER_ROLE)
            };
        }
    }

    public LoggedInUserModel LoggedInUser => new LoggedInUserModel() {
        Id = HttpContext.User.FindFirstValue(AppClaims.USER_ID).AsGuid(),
        Username = HttpContext.User.FindFirstValue(AppClaims.USERNAME),
        Role = UserRole.FromString(HttpContext.User.FindFirstValue(AppClaims.USER_ROLE))
    };
}