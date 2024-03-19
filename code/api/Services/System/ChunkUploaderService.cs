namespace Quality.Storage.Api.Services.System;

public class ChunkUploaderService(AppDatabase database, ILogger<ChunkUploaderService> logger)
{
    private readonly ILogger<ChunkUploaderService> _logger = logger;
    private readonly AppDatabase _database = database;

	public async Task<Guid> StartUploadSession() {
        return default;
    }
}