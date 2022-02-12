using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja_do_Duke.Migrations
{
    public partial class InserirQuantidadeLeiEEspacoInventarioUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "EspacoInventario",
                table: "AspNetUsers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeLei",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EspacoInventario",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QuantidadeLei",
                table: "AspNetUsers");
        }
    }
}
