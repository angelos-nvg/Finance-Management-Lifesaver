using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagementLifesaver.Migrations
{
    public partial class Investments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestedMoney = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    StartMoney = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    InvestmentType = table.Column<int>(nullable: false),
                    RoI = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_AccountId",
                table: "Investments",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");
        }
    }
}
