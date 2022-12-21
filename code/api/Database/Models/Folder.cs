namespace I2R.Storage.Api.Database.Models;

public class Folder : Base
{
    public string Name { get; set; }
    public List<File> Files { get; set; }
    public List<Permission> Permissions { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsBinned { get; set; }
}