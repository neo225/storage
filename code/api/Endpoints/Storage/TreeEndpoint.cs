namespace Quality.Storage.Api.Endpoints.Storage;

public class TreeEndpoint(AppDatabase database, IPaginationService pagination) : EndpointBase
{
	[HttpGet("~/storage/tree")]
	public async Task<ActionResult<KeysetPaginationResult<FileSystemEntry>>> Handle(Guid parent = default) {
		return Ok(await pagination.KeysetPaginateAsync(database.Folders
															   .Include(c => c.Files)
															   .ConditionalWhere(() => parent != default, folder => folder.ParentId == parent),
													   b => b.Descending(a => a.Name),
													   async id => await database.Folders.FirstOrDefaultAsync(c => c.Id == id.AsGuid()),
													   query => query.Select(p => new FileSystemEntry {
															   Id = p.Id,
															   Name = p.Name,
															   MimeType = SystemConstants.FolderMimeType,
															   SizeInBytes = -1,
															   Files = p.Files.Select(c => new FileSystemEntry {
																				Id = c.Id,
																				Name = c.Name,
																				MimeType = c.MimeType,
																				SizeInBytes = c.SizeInBytes
																		})
																		.ToList()
													   })));
	}
}
