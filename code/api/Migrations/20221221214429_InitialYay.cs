#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Quality.Storage.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialYay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "folders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isencrypted = table.Column<bool>(name: "is_encrypted", type: "boolean", nullable: false),
                    isbinned = table.Column<bool>(name: "is_binned", type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    lastmodifiedat = table.Column<DateTime>(name: "last_modified_at", type: "timestamp with time zone", nullable: true),
                    lastdeletedat = table.Column<DateTime>(name: "last_deleted_at", type: "timestamp with time zone", nullable: true),
                    owninguserid = table.Column<Guid>(name: "owning_user_id", type: "uuid", nullable: true),
                    lastmodifiedby = table.Column<Guid>(name: "last_modified_by", type: "uuid", nullable: true),
                    lastdeletedby = table.Column<Guid>(name: "last_deleted_by", type: "uuid", nullable: true),
                    createdby = table.Column<Guid>(name: "created_by", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_folders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    lastmodifiedat = table.Column<DateTime>(name: "last_modified_at", type: "timestamp with time zone", nullable: true),
                    lastdeletedat = table.Column<DateTime>(name: "last_deleted_at", type: "timestamp with time zone", nullable: true),
                    owninguserid = table.Column<Guid>(name: "owning_user_id", type: "uuid", nullable: true),
                    lastmodifiedby = table.Column<Guid>(name: "last_modified_by", type: "uuid", nullable: true),
                    lastdeletedby = table.Column<Guid>(name: "last_deleted_by", type: "uuid", nullable: true),
                    createdby = table.Column<Guid>(name: "created_by", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    mimetype = table.Column<string>(name: "mime_type", type: "text", nullable: false),
                    sizeinbytes = table.Column<long>(name: "size_in_bytes", type: "bigint", nullable: false),
                    folderid = table.Column<Guid>(name: "folder_id", type: "uuid", nullable: false),
                    isencrypted = table.Column<bool>(name: "is_encrypted", type: "boolean", nullable: false),
                    isbinned = table.Column<bool>(name: "is_binned", type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    lastmodifiedat = table.Column<DateTime>(name: "last_modified_at", type: "timestamp with time zone", nullable: true),
                    lastdeletedat = table.Column<DateTime>(name: "last_deleted_at", type: "timestamp with time zone", nullable: true),
                    owninguserid = table.Column<Guid>(name: "owning_user_id", type: "uuid", nullable: true),
                    lastmodifiedby = table.Column<Guid>(name: "last_modified_by", type: "uuid", nullable: true),
                    lastdeletedby = table.Column<Guid>(name: "last_deleted_by", type: "uuid", nullable: true),
                    createdby = table.Column<Guid>(name: "created_by", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                    table.ForeignKey(
                        name: "fk_files_folders_folder_id",
                        column: x => x.folderid,
                        principalTable: "folders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    lastloggedon = table.Column<DateTime>(name: "last_logged_on", type: "timestamp with time zone", nullable: true),
                    permissiongroupid = table.Column<Guid>(name: "permission_group_id", type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    lastmodifiedat = table.Column<DateTime>(name: "last_modified_at", type: "timestamp with time zone", nullable: true),
                    lastdeletedat = table.Column<DateTime>(name: "last_deleted_at", type: "timestamp with time zone", nullable: true),
                    owninguserid = table.Column<Guid>(name: "owning_user_id", type: "uuid", nullable: true),
                    lastmodifiedby = table.Column<Guid>(name: "last_modified_by", type: "uuid", nullable: true),
                    lastdeletedby = table.Column<Guid>(name: "last_deleted_by", type: "uuid", nullable: true),
                    createdby = table.Column<Guid>(name: "created_by", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_permission_groups_permission_group_id",
                        column: x => x.permissiongroupid,
                        principalTable: "permission_groups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contentid = table.Column<Guid>(name: "content_id", type: "uuid", nullable: false),
                    isfile = table.Column<bool>(name: "is_file", type: "boolean", nullable: false),
                    canread = table.Column<bool>(name: "can_read", type: "boolean", nullable: false),
                    canwrite = table.Column<bool>(name: "can_write", type: "boolean", nullable: false),
                    groupid = table.Column<Guid>(name: "group_id", type: "uuid", nullable: false),
                    fileid = table.Column<Guid>(name: "file_id", type: "uuid", nullable: true),
                    folderid = table.Column<Guid>(name: "folder_id", type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    lastmodifiedat = table.Column<DateTime>(name: "last_modified_at", type: "timestamp with time zone", nullable: true),
                    lastdeletedat = table.Column<DateTime>(name: "last_deleted_at", type: "timestamp with time zone", nullable: true),
                    owninguserid = table.Column<Guid>(name: "owning_user_id", type: "uuid", nullable: true),
                    lastmodifiedby = table.Column<Guid>(name: "last_modified_by", type: "uuid", nullable: true),
                    lastdeletedby = table.Column<Guid>(name: "last_deleted_by", type: "uuid", nullable: true),
                    createdby = table.Column<Guid>(name: "created_by", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_permissions_files_file_id",
                        column: x => x.fileid,
                        principalTable: "files",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_permissions_folders_folder_id",
                        column: x => x.folderid,
                        principalTable: "folders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_permissions_permission_groups_group_id",
                        column: x => x.groupid,
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_files_folder_id",
                table: "files",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_file_id",
                table: "permissions",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_folder_id",
                table: "permissions",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_group_id",
                table: "permissions",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_permission_group_id",
                table: "users",
                column: "permission_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "permission_groups");

            migrationBuilder.DropTable(
                name: "folders");
        }
    }
}
