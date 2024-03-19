namespace Quality.Storage.Api.Database.Models;

public class User : Base
{
	[MaxLength(100)]
	public string Username { get; set; }

	[MaxLength(100)]
	public string Password { get; set; }

	public UserRole Role { get; set; }

	[MaxLength(100)]
	public string FirstName { get; set; }

	[MaxLength(100)]
	public string LastName { get; set; }

	public DateTime? LastLoggedOn { get; set; }

	public IEnumerable<Claim> DefaultClaims() => new List<Claim> {
			new(AppClaims.USER_ID, Id.ToString()),
			new(AppClaims.USERNAME, Username),
			new(AppClaims.USER_ROLE, UserRoleHelper.ToString(Role))
	};
}
