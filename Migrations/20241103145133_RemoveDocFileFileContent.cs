using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDocFileFileContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFile_Documents_DocumentID",
                table: "DocumentFile");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFile_DocumentID",
                table: "DocumentFile");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "DocumentFile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileContent",
                table: "DocumentFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFile_DocumentID",
                table: "DocumentFile",
                column: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFile_Documents_DocumentID",
                table: "DocumentFile",
                column: "DocumentID",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
