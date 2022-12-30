namespace I2R.Storage.Api.Database.Models;

public class Permission : Base
{
    public Permission() { }

    public Permission(Guid createdBy) : base(createdBy) { }
    public Guid ContentId { get; set; }
    public bool IsFile { get; set; }
    public bool CanRead { get; set; }
    public bool CanWrite { get; set; }
    public Guid GroupId { get; set; }
    public PermissionGroup Group { get; set; }
}