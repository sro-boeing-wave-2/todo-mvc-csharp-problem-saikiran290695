using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeApplication.Migrations.NotesTry3
{
    public partial class ModifiedKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Notes_Id",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Notes_Id",
                table: "Labels");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Labels",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "NotesId",
                table: "Labels",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CheckLists",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "NotesId",
                table: "CheckLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NotesId",
                table: "Labels",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_NotesId",
                table: "CheckLists",
                column: "NotesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Notes_NotesId",
                table: "CheckLists",
                column: "NotesId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Notes_NotesId",
                table: "Labels",
                column: "NotesId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Notes_NotesId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Notes_NotesId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_NotesId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_CheckLists_NotesId",
                table: "CheckLists");

            migrationBuilder.DropColumn(
                name: "NotesId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "NotesId",
                table: "CheckLists");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Labels",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CheckLists",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Notes_Id",
                table: "CheckLists",
                column: "Id",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Notes_Id",
                table: "Labels",
                column: "Id",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
