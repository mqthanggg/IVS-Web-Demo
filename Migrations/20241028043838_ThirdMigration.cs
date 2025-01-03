using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocTime",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocUploader",
                table: "Documents");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocTime",
                table: "DocumentFile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileUploader",
                table: "DocumentFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocTime",
                table: "DocumentFile");

            migrationBuilder.DropColumn(
                name: "FileUploader",
                table: "DocumentFile");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocTime",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DocUploader",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DocTime", "DocUploader" },
                values: new object[] { new DateTime(2024, 10, 28, 11, 35, 0, 354, DateTimeKind.Local).AddTicks(3752), "Mai Quoc Thang" });
        }
    }
}
