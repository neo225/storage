using File = Quality.Storage.Api.Database.Models.File;

namespace Quality.Storage.Api.Database;

using File = File;

public class AppDatabase(DbContextOptions<AppDatabase> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
	public DbSet<File> Files { get; set; }
	public DbSet<Folder> Folders { get; set; }
	public DbSet<Permission> Permissions { get; set; }
	public DbSet<PermissionGroup> PermissionGroups { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<User>(e => {
			e.ToTable("users");
		});
		modelBuilder.Entity<File>(e => {
			e.HasMany(c => c.Permissions);
			e.HasOne(c => c.Folder);
			e.ToTable("files");
		});
		modelBuilder.Entity<Folder>(e => {
			e.HasMany(c => c.Files);
			e.HasMany(c => c.Permissions);
			e.HasOne(c => c.Parent);
			e.ToTable("folders");
		});
		modelBuilder.Entity<PermissionGroup>(e => {
			e.HasMany(c => c.Users);
			e.ToTable("permission_groups");
		});
		modelBuilder.Entity<Permission>(e => {
			e.HasOne(c => c.Group);
			e.ToTable("permissions");
		});
		base.OnModelCreating(modelBuilder);
	}
}
