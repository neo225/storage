using Quality.Storage.Api.Services.Abstractions;
using File = Quality.Storage.Api.Database.Models.File;

namespace Quality.Storage.Api.Endpoints.Storage;

public class UploadEndpoint(
		IResourceService resourceService,
		IStringLocalizer<SharedResources> localizer,
		AppDatabase database,
		StorageService storageService
) : EndpointBase
{
	public new class Request
	{
		public IFormFileCollection FormFileCollection { get; set; }
		public Guid FolderId { get; set; }
	}

	[HttpPost("~/storage/upload")]
	public async Task<ActionResult> Handle(Request request) {
		var folder = await storageService.GetFileSystemEntryAsync(request.FolderId);
		if (folder == default) {
			return NotFound();
		}

		var problem = new KnownProblemModel();
		foreach (var formFile in request.FormFileCollection) {
			if (!formFile.FileName.IsValidFileName()) {
				problem.AddError("file_" + formFile.Name, localizer["{fileName} is an invalid file name"]);
				continue;
			}

			if (problem.Errors.Count != 0) {
				return KnownProblem(problem);
			}

			var file = new File(LoggedInUser.Id) {
					Name = formFile.FileName,
					FolderId = folder.Id,
					MimeType = formFile.ContentType,
					SizeInBytes = formFile.Length,
					OwningUserId = LoggedInUser.Id
			};

			var id = new StorageBlobId {
					Id = file.Id,
					Bucket = LoggedInUser.Id
			};

			await resourceService.SetBlobAsync(id, formFile.OpenReadStream());
			await database.Files.AddAsync(file);
			await database.SaveChangesAsync();
		}

		return Ok(await storageService.GetFileSystemEntryAsync(folder.Id));
	}
}
