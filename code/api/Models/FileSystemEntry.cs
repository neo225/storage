namespace I2R.Storage.Api.Models;

public class FileSystemEntry
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long SizeInBytes { get; set; }
}