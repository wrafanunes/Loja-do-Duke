using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja_do_Duke.Migrations
{
    public partial class addInventoryOccupationToSupplyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantidadeLei",
                table: "AspNetUsers",
                newName: "Lei");

            migrationBuilder.RenameColumn(
                name: "EspacoInventario",
                table: "AspNetUsers",
                newName: "InventoryCapacity");

            migrationBuilder.AddColumn<short>(
                name: "InventoryOccupation",
                table: "Supplies",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryOccupation",
                table: "Supplies");

            migrationBuilder.RenameColumn(
                name: "Lei",
                table: "AspNetUsers",
                newName: "QuantidadeLei");

            migrationBuilder.RenameColumn(
                name: "InventoryCapacity",
                table: "AspNetUsers",
                newName: "EspacoInventario");
        }
    }
}
