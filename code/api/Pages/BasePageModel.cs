using Microsoft.AspNetCore.Mvc.Filters;

namespace Quality.Storage.Api.Pages;

public class BasePageModel : PageModel
{
	public LoggedInUserModel LoggedInUser => new(User);
	public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

	public override void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
		if (!(context.HttpContext.User.Identity?.IsAuthenticated ?? true) && !context.HttpContext.Request.Path.StartsWithSegments("/login")) {
			context.Result = new RedirectResult("/login");
		}

		base.OnPageHandlerExecuting(context);
	}
}
