using File = I2R.Storage.Api.Database.Models.File;

namespace I2R.Storage.Api.Endpoints.Storage;

public class UploadEndpoint : EndpointBase
{
    private readonly DefaultResourceService _resourceService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly AppDatabase _database;
    private readonly StorageService _storageService;

    public UploadEndpoint(DefaultResourceService resourceService, IStringLocalizer<SharedResources> localizer, AppDatabase database, StorageService storageService) {
        _resourceService = resourceService;
        _localizer = localizer;
        _database = database;
        _storageService = storageService;
    }

    public class Request
    {
        public IFormFileCollection FormFileCollection { get; set; }
        public Guid FolderId { get; set; }
    }

    [HttpPost("~/storage/upload")]
    public async Task<ActionResult> Handle(Request request) {
        var folder = await _storageService.GetFileSystemEntryAsync(request.FolderId);
        if (folder == default) {
            return NotFound();
        }
        
        var problem = new KnownProblemModel();
        foreach (var formFile in request.FormFileCollection) {
            if (!formFile.FileName.IsValidFileName()) {
                problem.AddError("file_" + formFile.Name, _localizer["{fileName} is an invalid file name"]);
                continue;
            }

            if (problem.Errors.Any()) {
                return KnownProblem(problem);
            }

            var file = new File(LoggedInUser.Id) {
                Name = formFile.FileName,
                FolderId = folder.Id,
                MimeType = formFile.ContentType,
                SizeInBytes = formFile.Length,
                OwningUserId = LoggedInUser.Id
            };
            var id = new StorageBlobId() {
                Id = file.Id,
                Bucket = LoggedInUser.Id
            };
            await _resourceService.SetBlobAsync(id, formFile.OpenReadStream());
            await _database.Files.AddAsync(file);
            await _database.SaveChangesAsync();
        }

        return Ok(await _storageService.GetFileSystemEntryAsync(folder.Id));
    }
}