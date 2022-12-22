using Microsoft.AspNetCore.Mvc.Filters;

namespace I2R.Storage.Api.Pages;

public class BasePageModel : PageModel
{
    public LoggedInUserModel LoggedInUser => new(User);
    public bool IsAutenticated => User.Identity?.IsAuthenticated ?? false;

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
        if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true) {
            context.Result = new RedirectResult("/login");
        }

        base.OnPageHandlerExecuting(context);
    }
}