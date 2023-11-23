using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagementLifesaver.Migrations
{
    public partial class Scope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Scope_ScopeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ScopeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Scope",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scope_UserId",
                table: "Scope",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Users_UserId",
                table: "Scope",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Users_UserId",
                table: "Scope");

            migrationBuilder.DropIndex(
                name: "IX_Scope_UserId",
                table: "Scope");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Scope");

            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ScopeId",
                table: "Users",
                column: "ScopeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Scope_ScopeId",
                table: "Users",
                column: "ScopeId",
                principalTable: "Scope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
