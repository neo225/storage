namespace I2R.Storage.Api.Endpoints._Root;

public class SessionEndpoint : Base
{
    [HttpGet("~/session")]
    public ActionResult<LoggedInUserModel.Public> Handle() {
        return LoggedInUser.ForThePeople(HttpContext);
    }
}