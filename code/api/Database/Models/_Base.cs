namespace Quality.Storage.Api.Database.Models;

public class Base
{
	protected Base() {
		Id = Guid.NewGuid();
		CreatedAt = AppDateTime.UtcNow;
	}

	protected Base(Guid createdBy) {
		Id = Guid.NewGuid();
		CreatedAt = AppDateTime.UtcNow;
		CreatedBy = createdBy;
	}

	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? LastModifiedAt { get; set; }
	public DateTime? LastDeletedAt { get; set; }
	public Guid? LastModifiedBy { get; set; }
	public Guid? LastDeletedBy { get; set; }
	public Guid? CreatedBy { get; set; }

	public void SetDeleted(Guid performingUserId = default) {
		LastDeletedAt = AppDateTime.UtcNow;
		LastDeletedBy = performingUserId;
	}

	public void SetModified(Guid performingUserId = default) {
		LastModifiedAt = AppDateTime.UtcNow;
		LastModifiedBy = performingUserId;
	}

	public abstract class WithOwner : Base
	{
		protected WithOwner() { }
		protected WithOwner(Guid createdBy) : base(createdBy) { }

		public Guid? OwningUserId { get; set; }

		public void SetOwner(Guid ownerId = default) {
			OwningUserId = ownerId;
		}
	}
}
