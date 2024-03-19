namespace Quality.Storage.Api.Database.Models;

public class PermissionGroup : Base.WithOwner
{
	public PermissionGroup() { }

	public PermissionGroup(Guid createdBy) : base(createdBy) { }

	[MaxLength(100)]
	public string Name { get; set; }

	[MaxLength(450)]
	public string Description { get; set; }

	public virtual List<User> Users { get; set; }
}
