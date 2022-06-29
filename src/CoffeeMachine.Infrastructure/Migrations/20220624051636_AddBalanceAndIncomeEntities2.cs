using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddBalanceAndIncomeEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("1fe2e037-3abd-4bd7-bb66-5f55161b66d0"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("3d90b8b8-4964-4244-8bbd-ddf26a816392"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("67c40396-b4cd-4671-b9ee-2fc693436ffc"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("7872b2ba-d640-414e-ad7e-e07b452e3fc8"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("a3aaa590-ebd7-4f77-abf7-24a04459cf14"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("d77f993c-0c67-407e-a8fb-a024eccfff3e"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("f587cfc4-7fc1-420d-8aaf-5fbcc30c58f5"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("23da9cba-55fd-440d-8ec9-649522561d75"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("b41ca9e8-64ac-4152-a0fb-3bb43400d0e1"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("ff34d08e-6f7e-4186-a2ce-d7ec7d4069ed"));

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    BalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    EarnedMoney = table.Column<int>(type: "integer", nullable: false),
                    CoffeeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.BalanceId);
                    table.ForeignKey(
                        name: "FK_Balances_Coffees_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "Coffees",
                        principalColumn: "CoffeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalIncome = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                });

            migrationBuilder.InsertData(
                table: "BanknoteCashBoxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("cd2d1469-6390-44c2-8efa-d47bceb99b4e"), 50, 50 },
                    { new Guid("8942c6a5-78be-43fc-ab2c-804ae4ecb24a"), 40, 100 },
                    { new Guid("d46227f4-1deb-4b7f-834c-a2810a716706"), 30, 200 },
                    { new Guid("004d57a0-c692-4468-a019-cc78ee70fc0e"), 20, 500 },
                    { new Guid("f1da1957-a339-4310-92ea-1bd7f0a213e3"), 15, 1000 },
                    { new Guid("b963b79a-9b1c-499e-9fb8-b8b911dea012"), 10, 2000 },
                    { new Guid("1f87ede8-8b5e-46d2-b823-08ccda166b60"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("cce8ea4e-56ae-42b8-b653-edbf06f80e09"), "Капучино", 600 },
                    { new Guid("f8c20893-4dcd-4eae-966a-01939b5f1898"), "Латте", 850 },
                    { new Guid("ef46484b-ffe3-4972-a597-5b3fe7d6399e"), "Американо", 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CoffeeId",
                table: "Balances",
                column: "CoffeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("004d57a0-c692-4468-a019-cc78ee70fc0e"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("1f87ede8-8b5e-46d2-b823-08ccda166b60"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("8942c6a5-78be-43fc-ab2c-804ae4ecb24a"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("b963b79a-9b1c-499e-9fb8-b8b911dea012"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("cd2d1469-6390-44c2-8efa-d47bceb99b4e"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("d46227f4-1deb-4b7f-834c-a2810a716706"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("f1da1957-a339-4310-92ea-1bd7f0a213e3"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("cce8ea4e-56ae-42b8-b653-edbf06f80e09"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("ef46484b-ffe3-4972-a597-5b3fe7d6399e"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("f8c20893-4dcd-4eae-966a-01939b5f1898"));

            migrationBuilder.InsertData(
                table: "BanknoteCashBoxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("f587cfc4-7fc1-420d-8aaf-5fbcc30c58f5"), 50, 50 },
                    { new Guid("a3aaa590-ebd7-4f77-abf7-24a04459cf14"), 40, 100 },
                    { new Guid("67c40396-b4cd-4671-b9ee-2fc693436ffc"), 30, 200 },
                    { new Guid("d77f993c-0c67-407e-a8fb-a024eccfff3e"), 20, 500 },
                    { new Guid("7872b2ba-d640-414e-ad7e-e07b452e3fc8"), 15, 1000 },
                    { new Guid("1fe2e037-3abd-4bd7-bb66-5f55161b66d0"), 10, 2000 },
                    { new Guid("3d90b8b8-4964-4244-8bbd-ddf26a816392"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("b41ca9e8-64ac-4152-a0fb-3bb43400d0e1"), "Капучино", 600 },
                    { new Guid("23da9cba-55fd-440d-8ec9-649522561d75"), "Латте", 850 },
                    { new Guid("ff34d08e-6f7e-4186-a2ce-d7ec7d4069ed"), "Американо", 900 }
                });
        }
    }
}
