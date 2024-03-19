namespace Quality.Storage.Api.Database.Models;

public class File : Base.WithOwner
{
	public File() { }

	public File(Guid createdBy) : base(createdBy) { }

	[MaxLength(200)]
	public string Name { get; set; }

	[MaxLength(100)]
	public string MimeType { get; set; }

	public long SizeInBytes { get; set; }
	public Folder Folder { get; set; }
	public Guid FolderId { get; set; }
	public bool IsEncrypted { get; set; }
	public bool IsBinned { get; set; }
	public virtual List<Permission> Permissions { get; set; }
}
