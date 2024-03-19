namespace Quality.Storage.Api.Database.Models;

public class Folder : Base.WithOwner
{
	public Folder() { }

	public Folder(Guid createdBy) : base(createdBy) { }

	[MaxLength(200)]
	public string Name { get; set; }

	public Folder Parent { get; set; }
	public Guid? ParentId { get; set; }
	public List<File> Files { get; set; }
	public virtual List<Permission> Permissions { get; set; }
	public bool IsEncrypted { get; set; }
	public bool IsBinned { get; set; }
}
