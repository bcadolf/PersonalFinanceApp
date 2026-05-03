using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionsAccountsBudgetsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_AspNetUsers_UserProfileId",
                table: "BudgetItems");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_UserProfileId",
                table: "BudgetItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId",
                table: "BudgetItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BudgetName = table.Column<string>(type: "TEXT", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCurrent = table.Column<bool>(type: "INTEGER", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_BudgetId",
                table: "BudgetItems",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_UserProfileId",
                table: "Budget",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Budget_BudgetId",
                table: "BudgetItems",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Budget_BudgetId",
                table: "BudgetItems");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_BudgetId",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "BudgetItems");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_UserProfileId",
                table: "BudgetItems",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_AspNetUsers_UserProfileId",
                table: "BudgetItems",
                column: "UserProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
