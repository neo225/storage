namespace Quality.Storage.Api.Endpoints.Account;

public class LogoutEndpoint(UserService userService) : EndpointBase
{
	[HttpGet("~/account/logout")]
    public async Task<ActionResult> Handle() {
        await userService.LogOutUserAsync(HttpContext);
        return Ok();
    }
}