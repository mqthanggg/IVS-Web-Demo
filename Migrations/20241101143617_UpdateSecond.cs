using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocTime",
                table: "DocumentFile",
                newName: "FileTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocTime",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocTime",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "FileTime",
                table: "DocumentFile",
                newName: "DocTime");
        }
    }
}
