namespace Quality.Storage.Api.Pages;

public class Home : BasePageModel
{
	public void OnGet() {
		ViewData["Title"] = "Home";
	}
}
