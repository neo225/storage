namespace I2R.Storage.Api.Database.Models;

public class Base
{
    public Base() {
        Id = Guid.NewGuid();
        CreatedAt = AppDateTime.UtcNow;
    }

    public Base(Guid createdBy) {
        Id = Guid.NewGuid();
        CreatedAt = AppDateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public DateTime? LastDeletedAt { get; set; }
    public Guid? OwningUserId { get; set; }
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
}