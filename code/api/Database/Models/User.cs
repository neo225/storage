using System.Security.Claims;

namespace I2R.Storage.Api.Database.Models;

public class User : Base
{
    public User() { }

    public User(Guid createdBy) : base(createdBy) { }
    public string Username { get; set; }
    public string Password { get; set; }
    public EUserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastLoggedOn { get; set; }


    public IEnumerable<Claim> DefaultClaims() => new List<Claim>() {
        new(AppClaims.USER_ID, Id.ToString()),
        new(AppClaims.USERNAME, Username),
        new(AppClaims.USER_ROLE, UserRole.ToString(Role))
    };
}