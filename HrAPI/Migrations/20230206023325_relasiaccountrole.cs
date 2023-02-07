using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrAPI.Migrations
{
    public partial class relasiaccountrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRolesAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNIK",
                table: "AccountRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoles_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNIK",
                table: "AccountRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AccountRolesAccounts",
                columns: table => new
                {
                    AccountRolesId = table.Column<int>(type: "int", nullable: false),
                    AccountsNIK = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRolesAccounts", x => new { x.AccountRolesId, x.AccountsNIK });
                    table.ForeignKey(
                        name: "FK_AccountRolesAccounts_AccountRoles_AccountRolesId",
                        column: x => x.AccountRolesId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRolesAccounts_Accounts_AccountsNIK",
                        column: x => x.AccountsNIK,
                        principalTable: "Accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRolesAccounts_AccountsNIK",
                table: "AccountRolesAccounts",
                column: "AccountsNIK");
        }
    }
}
