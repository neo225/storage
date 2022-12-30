namespace I2R.Storage.Api.Endpoints;

[ApiController]
[Authorize]
public class EndpointBase : ControllerBase
{
    protected LoggedInUserModel LoggedInUser => new(User);

    [NonAction]
    protected ActionResult KnownProblem(string title = default, string subtitle = default, Dictionary<string, string[]> errors = default) {
        HttpContext.Response.Headers.Add(AppHeaders.IS_KNOWN_PROBLEM, "1");
        return BadRequest(new KnownProblemModel {
            Title = title,
            Subtitle = subtitle,
            Errors = errors,
            TraceId = HttpContext.TraceIdentifier
        });
    }

    [NonAction]
    protected ActionResult KnownProblem(KnownProblemModel problem) {
        HttpContext.Response.Headers.Add(AppHeaders.IS_KNOWN_PROBLEM, "1");
        problem.TraceId = HttpContext.TraceIdentifier;
        return BadRequest(problem);
    }
}