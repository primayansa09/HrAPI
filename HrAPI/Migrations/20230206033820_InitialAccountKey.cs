using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrAPI.Migrations
{
    public partial class InitialAccountKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoles_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.AddColumn<string>(
                name: "AccountsNIK",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AccountRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNIK",
                table: "AccountRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                columns: new[] { "AccountNIK", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountsNIK",
                table: "Employees",
                column: "AccountsNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Accounts_AccountNIK",
                table: "AccountRoles",
                column: "AccountNIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accounts_AccountsNIK",
                table: "Employees",
                column: "AccountsNIK",
                principalTable: "Accounts",
                principalColumn: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accounts_AccountsNIK",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AccountsNIK",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.DropColumn(
                name: "AccountsNIK",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AccountRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNIK",
                table: "AccountRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_AccountNIK",
                table: "AccountRoles",
                column: "AccountNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Accounts_AccountNIK",
                table: "AccountRoles",
                column: "AccountNIK",
                principalTable: "Accounts",
                principalColumn: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
