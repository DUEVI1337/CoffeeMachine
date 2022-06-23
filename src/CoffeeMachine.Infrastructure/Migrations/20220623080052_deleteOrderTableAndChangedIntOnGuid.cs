using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class deleteOrderTableAndChangedIntOnGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanknoteCashBoxes",
                columns: table => new
                {
                    BanknoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountBanknote = table.Column<int>(type: "integer", nullable: false),
                    Denomination = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknoteCashBoxes", x => x.BanknoteId);
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
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashDepositAmount = table.Column<int>(type: "integer", nullable: false),
                    ContributedMoney = table.Column<int>(type: "integer", nullable: false),
                    CoffeeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Coffees_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "Coffees",
                        principalColumn: "CoffeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BanknoteCashBoxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("b16136a2-1652-40ec-95f2-2f55b7bbd854"), 50, 50 },
                    { new Guid("0e1899f5-c1e1-4018-a454-f56c6074d3ea"), 40, 100 },
                    { new Guid("6a491331-a10d-4ac1-b5d7-be6921d5cbab"), 30, 200 },
                    { new Guid("495e5fe8-943a-4fc4-8607-f6814731f31e"), 20, 500 },
                    { new Guid("fdb65f57-c3e2-42e5-b1f4-088cbed3e94d"), 15, 1000 },
                    { new Guid("bff16cca-ebef-4b65-a3ea-554d130e1777"), 10, 2000 },
                    { new Guid("f2328c2c-0c4f-4d79-addb-db534a2c5b1e"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("e801ef24-4af6-4bc9-a9bb-bd3efc040849"), "Капучино", 600 },
                    { new Guid("13f1d466-41eb-4f8c-9a67-e24dde4bae1e"), "Латте", 850 },
                    { new Guid("4dd54c3b-e12d-4ace-8920-6c7dcbc35cef"), "Американо", 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CoffeeId",
                table: "Payments",
                column: "CoffeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknoteCashBoxes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Coffees");
        }
    }
}
