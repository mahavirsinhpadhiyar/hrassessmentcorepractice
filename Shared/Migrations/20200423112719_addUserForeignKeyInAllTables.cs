using Microsoft.EntityFrameworkCore.Migrations;

namespace Shared.Migrations
{
    public partial class addUserForeignKeyInAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Consultants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Companys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_UserId",
                table: "Consultants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_UserId",
                table: "Companys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_AspNetUsers_UserId",
                table: "Companys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultants_AspNetUsers_UserId",
                table: "Consultants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companys_AspNetUsers_UserId",
                table: "Companys");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultants_AspNetUsers_UserId",
                table: "Consultants");

            migrationBuilder.DropIndex(
                name: "IX_Consultants_UserId",
                table: "Consultants");

            migrationBuilder.DropIndex(
                name: "IX_Companys_UserId",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companys");
        }
    }
}
