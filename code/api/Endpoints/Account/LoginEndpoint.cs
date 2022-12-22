namespace I2R.Storage.Api.Endpoints.Account;

public class LoginEndpoint : EndpointBase
{
    private readonly AppDatabase _database;
    private readonly UserService _userService;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public new class Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public LoginEndpoint(UserService userService, AppDatabase database, IStringLocalizer<SharedResources> localizer) {
        _userService = userService;
        _database = database;
        _localizer = localizer;
    }

    [AllowAnonymous]
    [HttpPost("~/account/login")]
    public async Task<ActionResult> Handle([FromBody] Request request) {
        var user = _database.Users.FirstOrDefault(c => c.Username == request.Username);
        if (user == default) {
            return KnownProblem(_localizer["Invalid username or password"]);
        }

        if (!PasswordHelper.Verify(request.Password, user.Password)) {
            return KnownProblem(_localizer["Invalid username or password"]);
        }

        await _userService.LogInUserAsync(HttpContext, user.DefaultClaims());
        return Ok();
    }
}