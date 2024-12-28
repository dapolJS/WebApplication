using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbWithNewModelBuilder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Notebook_NotebookId",
                schema: "NotesAppSchema",
                table: "Note");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Notebook_NotebookId",
                schema: "NotesAppSchema",
                table: "Note",
                column: "NotebookId",
                principalSchema: "NotesAppSchema",
                principalTable: "Notebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Notebook_NotebookId",
                schema: "NotesAppSchema",
                table: "Note");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Notebook_NotebookId",
                schema: "NotesAppSchema",
                table: "Note",
                column: "NotebookId",
                principalSchema: "NotesAppSchema",
                principalTable: "Notebook",
                principalColumn: "Id");
        }
    }
}
