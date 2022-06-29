using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class UpdateEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanknoteCashboxes",
                columns: table => new
                {
                    BanknoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountBanknote = table.Column<int>(type: "integer", nullable: false),
                    Denomination = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknoteCashboxes", x => x.BanknoteId);
                });

            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    CoffeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.CoffeeId);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalIncome = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    BalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoffeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EarnedMoney = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.BalanceId);
                    table.ForeignKey(
                        name: "FK_Balances_Coffees_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "Coffees",
                        principalColumn: "CoffeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoffeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientMoney = table.Column<int>(type: "integer", nullable: false),
                    Deal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Coffees_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "Coffees",
                        principalColumn: "CoffeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BanknoteCashboxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("df0c6023-58c2-4f9e-8cb6-57d3df7d95ff"), 50, 50 },
                    { new Guid("d0ef9196-b8c1-4650-a5b0-662a41dda7b9"), 40, 100 },
                    { new Guid("25d7c364-da5e-473a-a64c-ee19849fba34"), 30, 200 },
                    { new Guid("494c6473-63f0-40c3-9ace-61ea6487b5ec"), 20, 500 },
                    { new Guid("8040df98-0a0e-493c-aa1a-881ed63d7afd"), 15, 1000 },
                    { new Guid("e66c9691-7cba-4ee2-8b81-7e843512ea57"), 10, 2000 },
                    { new Guid("2e0f54d8-bb5d-49d4-929e-53e7de42b7fb"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("56dc0c27-a56d-4fb9-a899-83d28223fffe"), "Капучино", 600 },
                    { new Guid("0a0151ed-1e06-47ca-956d-8b61e05163eb"), "Латте", 850 },
                    { new Guid("78f439f3-0804-48f4-96cf-f22c67b2a943"), "Американо", 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CoffeeId",
                table: "Balances",
                column: "CoffeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CoffeeId",
                table: "Payments",
                column: "CoffeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "BanknoteCashboxes");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Coffees");
        }
    }
}
