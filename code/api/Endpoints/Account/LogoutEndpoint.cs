namespace I2R.Storage.Api.Endpoints.Account;

public class LogoutEndpoint : Base
{
    private readonly UserService _userService;

    public LogoutEndpoint(UserService userService) {
        _userService = userService;
    }

    [HttpGet("~/account/logout")]
    public async Task<ActionResult> Handle() {
        await _userService.LogOutUserAsync(HttpContext);
        return Ok();
    }
}