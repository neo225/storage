namespace Quality.Storage.Api.Models;

public class KnownProblemModel(string title = default, string subtitle = default, Dictionary<string, string[]> errors = default)
{
	public string Title { get; set; } = title;
	public string Subtitle { get; set; } = subtitle;
	public Dictionary<string, string[]> Errors { get; set; } = errors ?? new Dictionary<string, string[]>();
	public string TraceId { get; set; }

    public void AddError(string field, string errorText) {
        if (!Errors.ContainsKey(field)) {
            Errors.Add(field, new[] {errorText});
        } else {
            var currentErrors = Errors[field];
            var newErrors = currentErrors.Concat(new[] {errorText});
            Errors.Remove(field);
            Errors.Add(field, newErrors.ToArray());
        }
    }
}
