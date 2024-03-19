namespace Quality.Storage.Api.Models;

public struct StorageBlobId
{
    public Guid Id { get; set; }
    public Guid Bucket { get; set; }

    public override string ToString() {
        return Bucket + ":" + Id;
    }
}