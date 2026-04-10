using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateRAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentChunks",
                table: "DocumentChunks");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "documents");

            migrationBuilder.RenameTable(
                name: "DocumentChunks",
                newName: "document_chunks");

            migrationBuilder.AlterColumn<string>(
                name: "StoragePath",
                table: "documents",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "documents",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "documents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documents",
                table: "documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_document_chunks",
                table: "document_chunks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_document_chunks_DocumentId_ChunkIndex",
                table: "document_chunks",
                columns: new[] { "DocumentId", "ChunkIndex" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_documents",
                table: "documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_document_chunks",
                table: "document_chunks");

            migrationBuilder.DropIndex(
                name: "IX_document_chunks_DocumentId_ChunkIndex",
                table: "document_chunks");

            migrationBuilder.RenameTable(
                name: "documents",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "document_chunks",
                newName: "DocumentChunks");

            migrationBuilder.AlterColumn<string>(
                name: "StoragePath",
                table: "Documents",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Documents",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Documents",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentChunks",
                table: "DocumentChunks",
                column: "Id");
        }
    }
}
