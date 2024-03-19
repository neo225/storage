namespace Quality.Storage.Api.Endpoints.Account;

public class LoginEndpoint(UserService userService, AppDatabase database, IStringLocalizer<SharedResources> localizer) : EndpointBase
{
	public new class Request
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	[AllowAnonymous]
	[HttpPost("~/account/login")]
	public async Task<ActionResult> Handle([FromBody] Request request) {
		var user = database.Users.FirstOrDefault(c => c.Username == request.Username);
		if (user == default) {
			return KnownProblem(localizer["Invalid username or password"]);
		}

		if (!PasswordHelper.Verify(request.Password, user.Password)) {
			return KnownProblem(localizer["Invalid username or password"]);
		}

		await userService.LogInUserAsync(HttpContext, user.DefaultClaims());
		return Ok();
	}
}
