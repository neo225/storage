namespace I2R.Storage.Api.Database.Models;

public class PermissionGroup : Base
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }
}