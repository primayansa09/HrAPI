using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrAPI.Migrations
{
    public partial class InitialOnetoOneacc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accounts_AccountsNIK",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AccountsNIK",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AccountsNIK",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "AccountsNIK",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountsNIK",
                table: "Employees",
                column: "AccountsNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accounts_AccountsNIK",
                table: "Employees",
                column: "AccountsNIK",
                principalTable: "Accounts",
                principalColumn: "NIK");
        }
    }
}
