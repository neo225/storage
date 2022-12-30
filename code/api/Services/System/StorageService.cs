namespace I2R.Storage.Api.Services.System;

public class StorageService
{
    private readonly AppDatabase _database;
    private readonly ILogger<StorageService> _logger;

    public StorageService(AppDatabase database, ILogger<StorageService> logger) {
        _database = database;
        _logger = logger;
    }

    public async Task<List<FileSystemEntry>> GetFileSystemEntriesAsync(Guid parent = default) {
        var fileSystemEntriesContext = _database.Folders
            .Include(c => c.Files)
            .ConditionalWhere(() => parent != default, folder => folder.ParentId == parent)
            .Select(p => new FileSystemEntry() {
                Id = p.Id,
                Name = p.Name,
                MimeType = SystemConstants.FolderMimeType,
                SizeInBytes = -1,
                Files = p.Files.Select(c => new FileSystemEntry() {
                    Id = c.Id,
                    Name = c.Name,
                    MimeType = c.MimeType,
                    SizeInBytes = c.SizeInBytes
                }).ToList()
            })
            .KeysetPaginate(builder => { builder.Ascending(entry => entry.Name); });
        var fileSystemEntries = await fileSystemEntriesContext.Query.ToListAsync();
        fileSystemEntriesContext.EnsureCorrectOrder(fileSystemEntries);
        return fileSystemEntries;
    }

    public async Task<FileSystemEntry> GetFileSystemEntryAsync(Guid folder) {
        return _database.Folders.Include(c => c.Files).Select(c => new FileSystemEntry() {
            Id = c.Id,
            SizeInBytes = -1,
            MimeType = SystemConstants.FolderMimeType,
            Files = c.Files.Select(p => new FileSystemEntry() {
                SizeInBytes = p.SizeInBytes,
                MimeType = p.MimeType,
                Id = p.Id,
                Name = p.Name
            }).ToList()
        }).FirstOrDefault(c => c.Id == folder);
    }
}