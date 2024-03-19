namespace Quality.Storage.Api.Pages.Shared.Components.Header;

public class Header(IStringLocalizer<SharedResources> localizer) : ViewComponent
{
	public string Home { get; set; }
	private readonly IStringLocalizer<SharedResources> _localizer = localizer;

	public async Task<IViewComponentResult> InvokeAsync() {
		return View();
	}
}
