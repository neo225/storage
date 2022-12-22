namespace I2R.Storage.Api.Endpoints.Account;

public class DeleteEndpoint : EndpointBase
{
    private readonly UserService _userService;

    public DeleteEndpoint(UserService userService) {
        _userService = userService;
    }

    [HttpDelete("~/account/delete")]
    public async Task<ActionResult> Handle() {
        await _userService.MarkUserAsDeletedAsync(LoggedInUser.Id, LoggedInUser.Id);
        await _userService.LogOutUserAsync(HttpContext);
        return Ok();
    }
}