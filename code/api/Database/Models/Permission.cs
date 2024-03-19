namespace Quality.Storage.Api.Database.Models;

public class Permission : Base.WithOwner
{
	public Permission() { }

	public Permission(Guid createdBy) : base(createdBy) { }
	public Guid ContentId { get; set; }
	public bool IsFile { get; set; }
	public bool CanRead { get; set; }
	public bool CanWrite { get; set; }
	public Guid GroupId { get; set; }
	public virtual PermissionGroup Group { get; set; }
}
