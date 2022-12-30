namespace I2R.Storage.Api.Services.System;

public class ChunkUploaderService
{
    private readonly ILogger<ChunkUploaderService> _logger;
    private readonly AppDatabase _database;

    public ChunkUploaderService(AppDatabase database, ILogger<ChunkUploaderService> logger) {
        _database = database;
        _logger = logger;
    }

    public async Task<Guid> StartUploadSession() {
        return default;
    }
}