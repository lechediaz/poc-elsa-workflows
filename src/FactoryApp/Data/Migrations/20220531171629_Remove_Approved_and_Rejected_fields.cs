using Microsoft.EntityFrameworkCore.Migrations;

namespace FactoryApp.Data.Migrations
{
    public partial class Remove_Approved_and_Rejected_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AproveLink",
                table: "Requests",
                newName: "ApproveLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApproveLink",
                table: "Requests",
                newName: "AproveLink");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Requests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "Requests",
                type: "bit",
                nullable: true);
        }
    }
}
