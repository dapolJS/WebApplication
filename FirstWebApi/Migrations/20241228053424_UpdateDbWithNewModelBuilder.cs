using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbWithNewModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebook_Room_RoomId",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.DropIndex(
                name: "IX_Notebook_RoomId",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueKey",
                schema: "NotesAppSchema",
                table: "Room",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Room_UniqueKey",
                schema: "NotesAppSchema",
                table: "Room",
                column: "UniqueKey");

            migrationBuilder.CreateIndex(
                name: "IX_Notebook_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook",
                column: "RoomUniqueKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Notebook_Room_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook",
                column: "RoomUniqueKey",
                principalSchema: "NotesAppSchema",
                principalTable: "Room",
                principalColumn: "UniqueKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebook_Room_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Room_UniqueKey",
                schema: "NotesAppSchema",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Notebook_RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.DropColumn(
                name: "RoomUniqueKey",
                schema: "NotesAppSchema",
                table: "Notebook");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueKey",
                schema: "NotesAppSchema",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                schema: "NotesAppSchema",
                table: "Notebook",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notebook_RoomId",
                schema: "NotesAppSchema",
                table: "Notebook",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notebook_Room_RoomId",
                schema: "NotesAppSchema",
                table: "Notebook",
                column: "RoomId",
                principalSchema: "NotesAppSchema",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
