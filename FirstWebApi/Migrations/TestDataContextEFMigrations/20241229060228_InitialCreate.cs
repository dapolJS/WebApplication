using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApi.Migrations.TestDataContextEFMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NotesAppSchema");

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "NotesAppSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueKey = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.UniqueConstraint("AK_Rooms_UniqueKey", x => x.UniqueKey);
                });

            migrationBuilder.CreateTable(
                name: "Notebooks",
                schema: "NotesAppSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomUniqueKey = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notebooks_Rooms_RoomUniqueKey",
                        column: x => x.RoomUniqueKey,
                        principalSchema: "NotesAppSchema",
                        principalTable: "Rooms",
                        principalColumn: "UniqueKey");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "NotesAppSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotebookId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalSchema: "NotesAppSchema",
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebooks",
                column: "RoomUniqueKey");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NotebookId",
                schema: "NotesAppSchema",
                table: "Notes",
                column: "NotebookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes",
                schema: "NotesAppSchema");

            migrationBuilder.DropTable(
                name: "Notebooks",
                schema: "NotesAppSchema");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "NotesAppSchema");
        }
    }
}
