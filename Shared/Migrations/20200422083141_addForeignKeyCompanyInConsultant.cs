using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shared.Migrations
{
    public partial class addForeignKeyCompanyInConsultant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserModels",
                keyColumn: "Id",
                keyValue: new Guid("548c1524-eb2a-406c-bf8c-6ad8fd20ce48"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Consultants",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_CompanyId",
                table: "Consultants",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultants_Companys_CompanyId",
                table: "Consultants",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultants_Companys_CompanyId",
                table: "Consultants");

            migrationBuilder.DropIndex(
                name: "IX_Consultants_CompanyId",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Consultants");

            migrationBuilder.InsertData(
                table: "UserModels",
                columns: new[] { "Id", "CreatedOn", "DOB", "Email", "FirstName", "LastName", "Password", "Token" },
                values: new object[] { new Guid("548c1524-eb2a-406c-bf8c-6ad8fd20ce48"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 21, 22, 24, 50, 874, DateTimeKind.Local).AddTicks(8599), "admin@gmail.com", "admin", "admin", "Admin@123", null });
        }
    }
}
