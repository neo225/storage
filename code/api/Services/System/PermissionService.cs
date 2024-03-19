namespace Quality.Storage.Api.Services.System;

public class PermissionService(ILogger<PermissionService> logger, AppDatabase database)
{
	private readonly AppDatabase _database = database;
	private readonly ILogger<PermissionService> _logger = logger;

	public async Task GrantAllOnFile(Guid fileId, Guid userId) { }
	public async Task GrantAllOnFolder(Guid fileId, Guid userId) { }
	public async Task UserCanReadFile(Guid fileId, Guid userId) { }
	public async Task UserCanReadFolder(Guid folderId, Guid userId) { }
	public async Task UserCanWriteFile(Guid fileId, Guid userId) { }
	public async Task UserCanWriteFolder(Guid folderId, Guid userId) { }
}
