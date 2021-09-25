using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja_do_Duke.Migrations
{
    public partial class creatingForeignKeyForSupply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Categories_CategoryId",
                table: "Supplies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Supplies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Categories_CategoryId",
                table: "Supplies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Categories_CategoryId",
                table: "Supplies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Supplies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Categories_CategoryId",
                table: "Supplies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
