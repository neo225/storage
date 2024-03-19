using Quality.Storage.Api.Services.Abstractions;
using File = System.IO.File;

namespace Quality.Storage.Api.Services.System;

public class DefaultResourceService(IConfiguration configuration) : IResourceService
{
	public async Task SetBlobAsync(StorageBlobId id, Stream stream, CancellationToken cancellationToken = default) {
        await stream.CopyToAsync(File.OpenWrite(EnsureCreatedAndReturnBasedPath(id)), cancellationToken);
    }

    public Task<byte[]> GetBlobAsync(StorageBlobId id, CancellationToken cancellationToken = default) {
        return File.ReadAllBytesAsync(EnsureCreatedAndReturnBasedPath(id), cancellationToken);
    }

    public Task RemoveBlobAsync(StorageBlobId id, CancellationToken cancellationToken = default) {
        File.Delete(EnsureCreatedAndReturnBasedPath(id));
        return Task.CompletedTask;
    }

    public async Task SetBlobMetadataAsync(StorageBlobId id, Dictionary<string, string> metadata, CancellationToken cancellationToken = default) {
        await File.OpenWrite(EnsureCreatedAndReturnBasedPath(id) + SystemConstants.MetadataFileExtension).WriteAsync(JsonSerializer.SerializeToUtf8Bytes(metadata), cancellationToken);
    }

    public async Task<Dictionary<string, string>> GetBlobMetadataAsync(StorageBlobId id, CancellationToken cancellationToken = default) {
        return JsonSerializer.Deserialize<Dictionary<string, string>>(await File.ReadAllBytesAsync(EnsureCreatedAndReturnBasedPath(id) + SystemConstants.MetadataFileExtension, cancellationToken));
    }

    private string EnsureCreatedAndReturnBasedPath(StorageBlobId id) {
        var withoutId = Path.Combine(Directory.GetCurrentDirectory(), configuration.GetValue(AppEnvVariables.STORAGE_ROOT, "__FILESYSTEM__"), id.Bucket.ToString());
        Directory.CreateDirectory(withoutId);
        return Path.Combine(withoutId, id.Id.ToString());
    }
}