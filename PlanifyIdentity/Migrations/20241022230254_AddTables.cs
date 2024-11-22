using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PlanifyIdentity.Migrations;

/// <inheritdoc />
internal sealed partial class AddTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.EnsureSchema(
            name: "identity");

        _ = migrationBuilder.CreateTable(
            name: "Role",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: static table => table.PrimaryKey("PK_Role", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Status",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Entity = table.Column<string>(type: "text", nullable: true),
                Name = table.Column<string>(type: "text", nullable: true),
                Order = table.Column<int>(type: "integer", nullable: true),
                IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true),
                Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: true),
                LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                LastModifiedBy = table.Column<string>(type: "text", nullable: true)
            },
            constraints: static table => table.PrimaryKey("PK_Status", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "RoleClaims",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RoleId = table.Column<string>(type: "text", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: static table =>
            {
                _ = table.PrimaryKey("PK_RoleClaims", static x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_RoleClaims_Role_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "identity",
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "User",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<string>(type: "text", nullable: false),
                Locked = table.Column<bool>(type: "boolean", nullable: false),
                StatusId = table.Column<int>(type: "integer", nullable: false),
                PasswordExpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: true),
                UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: true),
                SecurityStamp = table.Column<string>(type: "text", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                PhoneNumber = table.Column<string>(type: "text", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_User", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_User_Status_StatusId",
                    column: x => x.StatusId,
                    principalSchema: "identity",
                    principalTable: "Status",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "UserClaim",
            schema: "identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<string>(type: "text", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: static table =>
            {
                _ = table.PrimaryKey("PK_UserClaim", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_UserClaim_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "identity",
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "UserLogin",
            schema: "identity",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                ProviderKey = table.Column<string>(type: "text", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                UserId = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                _ = table.ForeignKey(
                    name: "FK_UserLogin_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "identity",
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "UserRole",
            schema: "identity",
            columns: table => new
            {
                UserId = table.Column<string>(type: "text", nullable: false),
                RoleId = table.Column<string>(type: "text", nullable: false)
            },
            constraints: static table =>
            {
                _ = table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                _ = table.ForeignKey(
                    name: "FK_UserRole_Role_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "identity",
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_UserRole_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "identity",
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "UserToken",
            schema: "identity",
            columns: table => new
            {
                UserId = table.Column<string>(type: "text", nullable: false),
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: static table =>
            {
                _ = table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                _ = table.ForeignKey(
                    name: "FK_UserToken_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "identity",
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "identity",
            table: "Role",
            column: "NormalizedName",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_RoleClaims_RoleId",
            schema: "identity",
            table: "RoleClaims",
            column: "RoleId");

        _ = migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "identity",
            table: "User",
            column: "NormalizedEmail");

        _ = migrationBuilder.CreateIndex(
            name: "IX_User_StatusId",
            schema: "identity",
            table: "User",
            column: "StatusId");

        _ = migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "identity",
            table: "User",
            column: "NormalizedUserName",
            unique: true);

        _ = migrationBuilder.CreateIndex(
            name: "IX_UserClaim_UserId",
            schema: "identity",
            table: "UserClaim",
            column: "UserId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_UserLogin_UserId",
            schema: "identity",
            table: "UserLogin",
            column: "UserId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_UserRole_RoleId",
            schema: "identity",
            table: "UserRole",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "RoleClaims",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "UserClaim",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "UserLogin",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "UserRole",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "UserToken",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "Role",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "User",
            schema: "identity");

        _ = migrationBuilder.DropTable(
            name: "Status",
            schema: "identity");
    }
}
