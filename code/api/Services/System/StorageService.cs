using MR.AspNetCore.Pagination;
using MR.EntityFrameworkCore.KeysetPagination;

namespace I2R.Storage.Api.Services.System;

public class StorageService
{
    private readonly AppDatabase _database;
    private readonly ILogger<StorageService> _logger;

    public StorageService(AppDatabase database, ILogger<StorageService> logger) {
        _database = database;
        _logger = logger;
    }

    public async Task<KeysetPaginationResult<FileSystemEntry>> GetFileSystemEntriesAsync(Guid parent = default) {
        var keysetQuery = _database.Folders
            .Include(c => c.Files)
            .ConditionalWhere(() => parent != default, folder => folder.ParentId == parent)
            .Select(p => new FileSystemEntry() { });
        return default;
    }
}