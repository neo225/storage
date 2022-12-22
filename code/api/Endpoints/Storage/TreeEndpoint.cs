namespace I2R.Storage.Api.Endpoints.Storage;

public class TreeEndpoint : EndpointBase
{
    [HttpGet("~/storage/tree")]
    public async Task<ActionResult> Handle(Guid parent = default) {
        return Ok();
    }
}