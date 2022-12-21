namespace I2R.Storage.Api.Endpoints.Account;

public class CreateEndpoint : Base
{
    private readonly AppDatabase _database;
    private readonly UserService _userService;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CreateEndpoint(AppDatabase database, UserService userService, IStringLocalizer<SharedResources> localizer) {
        _database = database;
        _userService = userService;
        _localizer = localizer;
    }

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
        public EUserRole Role { get; set; }
    }

    [AllowAnonymous]
    [HttpPost("~/account/create")]
    public ActionResult Handle([FromBody] Request request) {
        if (!_userService.CanCreateAccount(request.Username)) {
            return BadRequest(_localizer["That username is already taken"]);
        }

        var user = new User() {
            Username = request.Username,
            Password = PasswordHelper.HashPassword(request.Password),
            LastName = request.LastName,
            FirstName = request.FirstName,
            Role = EUserRole.LEAST_PRIVILEGED,
        };
        _database.Users.Add(user);
        _database.SaveChanges();
        return Ok(new Response {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        });
    }
}