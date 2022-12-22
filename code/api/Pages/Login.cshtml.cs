namespace I2R.Storage.Api.Pages;

public class Login : PageModel
{
    public ActionResult OnGet() {
        if (User.Identity?.IsAuthenticated ?? false) {
            return Redirect("/home");
        }

        return Page();
    }
}