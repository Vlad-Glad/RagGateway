using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateRAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChunkPageRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageNumber",
                table: "document_chunks",
                newName: "StartPageNumber");

            migrationBuilder.AddColumn<int>(
                name: "EndPageNumber",
                table: "document_chunks",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndPageNumber",
                table: "document_chunks");

            migrationBuilder.RenameColumn(
                name: "StartPageNumber",
                table: "document_chunks",
                newName: "PageNumber");
        }
    }
}
