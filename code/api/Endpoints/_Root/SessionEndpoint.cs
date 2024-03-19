namespace Quality.Storage.Api.Endpoints._Root;

public class SessionEndpoint : EndpointBase
{
	[HttpGet("~/session")]
	public ActionResult<LoggedInUserModel.Public> Handle() {
		return LoggedInUserModel.ForThePeople(HttpContext);
	}
}
