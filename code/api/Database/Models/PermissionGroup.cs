namespace I2R.Storage.Api.Database.Models;

public class PermissionGroup : Base
{
    public PermissionGroup() { }

    public PermissionGroup(Guid createdBy) : base(createdBy) { }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }
}