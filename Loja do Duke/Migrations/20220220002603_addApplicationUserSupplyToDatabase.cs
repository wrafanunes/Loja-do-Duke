using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja_do_Duke.Migrations
{
    public partial class addApplicationUserSupplyToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryQuantity",
                table: "Supplies");

            migrationBuilder.CreateTable(
                name: "ApplicationUserSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplyId = table.Column<int>(type: "int", nullable: false),
                    UserInventoryQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSupplies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSupplies");

            migrationBuilder.AddColumn<int>(
                name: "InventoryQuantity",
                table: "Supplies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
