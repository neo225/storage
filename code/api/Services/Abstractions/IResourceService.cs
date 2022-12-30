using I2R.Storage.Api.Services.System;

namespace I2R.Storage.Api.Services.Abstractions;

public interface IResourceService
{
    public Task SetBlobAsync(StorageBlobId id, Stream stream, CancellationToken cancellationToken = default);
    public Task<byte[]> GetBlobAsync(StorageBlobId id, CancellationToken cancellationToken = default);
    public Task RemoveBlobAsync(StorageBlobId id, CancellationToken cancellationToken = default);
    public Task SetBlobMetadataAsync(StorageBlobId id, Dictionary<string, string> metadata, CancellationToken cancellationToken = default);
    public Task<Dictionary<string,string>> GetBlobMetadataAsync(StorageBlobId id, CancellationToken cancellationToken = default);
}