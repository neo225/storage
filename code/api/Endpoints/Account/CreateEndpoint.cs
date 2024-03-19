namespace Quality.Storage.Api.Endpoints.Account;

public class CreateEndpoint(AppDatabase database, UserService userService, IStringLocalizer<SharedResources> localizer) : EndpointBase
{
	public new class Request
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public new class Response
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public UserRole Role { get; set; }
	}

	[AllowAnonymous]
	[HttpPost("~/account/create")]
	public ActionResult Handle([FromBody] Request request) {
		if (!userService.CanCreateAccount(request.Username)) {
			return BadRequest(localizer["That username is already taken"]);
		}

		var user = new User {
				Username = request.Username,
				Password = PasswordHelper.HashPassword(request.Password),
				LastName = request.LastName,
				FirstName = request.FirstName,
				Role = UserRole.LEAST_PRIVILEGED,
		};
		database.Users.Add(user);
		database.SaveChanges();
		return Ok(new Response {
				Id = user.Id,
				Username = user.Username,
				Role = user.Role
		});
	}
}
