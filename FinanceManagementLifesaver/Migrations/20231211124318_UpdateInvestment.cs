using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagementLifesaver.Migrations
{
    public partial class UpdateInvestment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Investments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Investments");
        }
    }
}
