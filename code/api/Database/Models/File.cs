namespace I2R.Storage.Api.Database.Models;

public class File : Base
{
    public string Name { get; set; }
    public string MimeType { get; set; }
    public long SizeInBytes { get; set; }
    public Folder Folder { get; set; }
    public Guid FolderId { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsBinned { get; set; }
    public List<Permission> Permissions { get; set; }
}