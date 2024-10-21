using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Planify.Identity.Migrations;

/// <inheritdoc />
public partial class AddUserFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "Created",
            schema: "identity",
            table: "User",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            schema: "identity",
            table: "User",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "Locked",
            schema: "identity",
            table: "User",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "PasswordExpDate",
            schema: "identity",
            table: "User",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<int>(
            name: "StatusId",
            schema: "identity",
            table: "User",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
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
            constraints: table => table.PrimaryKey("PK_Status", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_User_StatusId",
            schema: "identity",
            table: "User",
            column: "StatusId");

        migrationBuilder.AddForeignKey(
            name: "FK_User_Status_StatusId",
            schema: "identity",
            table: "User",
            column: "StatusId",
            principalSchema: "identity",
            principalTable: "Status",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

        migrationBuilder.DropTable(
            name: "Status",
            schema: "identity");

        migrationBuilder.DropColumn(
            name: "Created",
            schema: "identity",
            table: "User");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            schema: "identity",
            table: "User");

        migrationBuilder.DropColumn(
            name: "Locked",
            schema: "identity",
            table: "User");

        migrationBuilder.DropColumn(
            name: "PasswordExpDate",
            schema: "identity",
            table: "User");

        migrationBuilder.DropColumn(
            name: "StatusId",
            schema: "identity",
            table: "User");

        migrationBuilder.AddColumn<string>(
            name: "Initials",
            schema: "identity",
            table: "User",
            type: "character varying(5)",
            maxLength: 5,
            nullable: true);
    }
}
