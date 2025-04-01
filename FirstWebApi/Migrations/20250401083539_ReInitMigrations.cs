using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ReInitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NotesAppSchema");

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "NotesAppSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueKey = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.UniqueConstraint("AK_Room_UniqueKey", x => x.UniqueKey);
                });

            migrationBuilder.CreateTable(
                name: "Notebook",
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
                    table.PrimaryKey("PK_Notebook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notebook_Room_RoomUniqueKey",
                        column: x => x.RoomUniqueKey,
                        principalSchema: "NotesAppSchema",
                        principalTable: "Room",
                        principalColumn: "UniqueKey");
                });

            migrationBuilder.CreateTable(
                name: "Note",
                schema: "NotesAppSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotebookId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Notebook_NotebookId",
                        column: x => x.NotebookId,
                        principalSchema: "NotesAppSchema",
                        principalTable: "Notebook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_NotebookId",
                schema: "NotesAppSchema",
                table: "Note",
                column: "NotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebook_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook",
                column: "RoomUniqueKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note",
                schema: "NotesAppSchema");

            migrationBuilder.DropTable(
                name: "Notebook",
                schema: "NotesAppSchema");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "NotesAppSchema");
        }
    }
}
