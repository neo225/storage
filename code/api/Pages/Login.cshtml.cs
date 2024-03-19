namespace Quality.Storage.Api.Pages;

public class Login : BasePageModel
{
	public ActionResult OnGet() {
		ViewData["Title"] = "Login";
		if (IsAuthenticated) return Redirect("/home");
		return Page();
	}
}
