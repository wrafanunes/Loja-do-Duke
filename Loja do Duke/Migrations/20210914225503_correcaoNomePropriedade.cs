using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja_do_Duke.Migrations
{
    public partial class correcaoNomePropriedade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "ApplicationTypes",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplicationTypes",
                newName: "CategoryName");
        }
    }
}
