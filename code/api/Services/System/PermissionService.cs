namespace I2R.Storage.Api.Services.System;

public class PermissionService
{
    private readonly AppDatabase _database;
    private readonly ILogger<PermissionService> _logger;

    public PermissionService(ILogger<PermissionService> logger, AppDatabase database) {
        _logger = logger;
        _database = database;
    }

    public async Task GrantAllOnFile(Guid fileId, Guid userId) { }
    public async Task GrantAllOnFolder(Guid fileId, Guid userId) { }
    public async Task UserCanReadFile(Guid fileId, Guid userId) { }
    public async Task UserCanReadFolder(Guid folderId, Guid userId) { }
    public async Task UserCanWriteFile(Guid fileId, Guid userId) { }
    public async Task UserCanWriteFolder(Guid folderId, Guid userId) { }
}