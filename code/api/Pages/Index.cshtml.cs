namespace I2R.Storage.Api.Pages;

public class Index : PageModel
{
    public ActionResult OnGet() {
        return User.Identity.IsAuthenticated ? Redirect("/home") : Redirect("/login");
    }
}