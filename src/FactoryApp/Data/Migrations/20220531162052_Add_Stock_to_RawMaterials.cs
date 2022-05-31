using Microsoft.EntityFrameworkCore.Migrations;

namespace FactoryApp.Data.Migrations
{
    public partial class Add_Stock_to_RawMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Stock",
                table: "RawMaterials",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "RawMaterials");
        }
    }
}
