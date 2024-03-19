namespace Quality.Storage.Api.Endpoints.Account;

public class DeleteEndpoint(UserService userService) : EndpointBase
{
	[HttpDelete("~/account/delete")]
	public async Task<ActionResult> Handle() {
		await userService.MarkUserAsDeletedAsync(LoggedInUser.Id, LoggedInUser.Id);
		await userService.LogOutUserAsync(HttpContext);
		return Ok();
	}
}
