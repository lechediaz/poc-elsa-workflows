using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FactoryApp.Data.Migrations
{
    public partial class Rename_Request_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_CreatedById",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_ReceiverId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Requests",
                newName: "ApproverId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Requests",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ReceiverId",
                table: "Requests",
                newName: "IX_Requests_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_CreatedById",
                table: "Requests",
                newName: "IX_Requests_ApproverId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InNegociationAt",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InShipmentAt",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_ApproverId",
                table: "Requests",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_AuthorId",
                table: "Requests",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_ApproverId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_AuthorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "InNegociationAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "InShipmentAt",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Requests",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Requests",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AuthorId",
                table: "Requests",
                newName: "IX_Requests_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ApproverId",
                table: "Requests",
                newName: "IX_Requests_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_CreatedById",
                table: "Requests",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_ReceiverId",
                table: "Requests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
