namespace I2R.Storage.Api.Models;

public class FileSystemEntry
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MimeType { get; set; }
    public long SizeInBytes { get; set; }
    public List<FileSystemEntry> Files { get; set; }
}