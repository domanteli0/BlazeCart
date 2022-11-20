using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    public partial class Removeparent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                newName: "IX_Categories_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Merch",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Merch",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Merch",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Merch",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
