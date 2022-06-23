using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class disableIdentityForEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("0e1899f5-c1e1-4018-a454-f56c6074d3ea"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("495e5fe8-943a-4fc4-8607-f6814731f31e"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("6a491331-a10d-4ac1-b5d7-be6921d5cbab"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("b16136a2-1652-40ec-95f2-2f55b7bbd854"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("bff16cca-ebef-4b65-a3ea-554d130e1777"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("f2328c2c-0c4f-4d79-addb-db534a2c5b1e"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("fdb65f57-c3e2-42e5-b1f4-088cbed3e94d"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("13f1d466-41eb-4f8c-9a67-e24dde4bae1e"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("4dd54c3b-e12d-4ace-8920-6c7dcbc35cef"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("e801ef24-4af6-4bc9-a9bb-bd3efc040849"));

            migrationBuilder.InsertData(
                table: "BanknoteCashBoxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("8cdf142c-dfac-46e8-a38e-cd2d80d5361d"), 50, 50 },
                    { new Guid("cc803d87-1a88-427b-9cae-a2228ca6b2b1"), 40, 100 },
                    { new Guid("a0925e47-3b69-41f3-8e06-3528e9ecb1ee"), 30, 200 },
                    { new Guid("0589be88-4cac-43cf-8533-463a53bbbf51"), 20, 500 },
                    { new Guid("71e0ca92-82f3-447b-ba50-5e376299f52f"), 15, 1000 },
                    { new Guid("69184024-5450-4a67-9bb5-37d0c625c3d9"), 10, 2000 },
                    { new Guid("2f6bb2f9-26b3-49ae-a6fc-a93afa917631"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("bb30094b-c15b-46aa-b172-f9e6ee67ed68"), "Капучино", 600 },
                    { new Guid("d242744b-e228-4642-b93b-c40c0163182a"), "Латте", 850 },
                    { new Guid("49d6ad06-225b-45e0-bbce-06496b4a14b1"), "Американо", 900 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("0589be88-4cac-43cf-8533-463a53bbbf51"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("2f6bb2f9-26b3-49ae-a6fc-a93afa917631"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("69184024-5450-4a67-9bb5-37d0c625c3d9"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("71e0ca92-82f3-447b-ba50-5e376299f52f"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("8cdf142c-dfac-46e8-a38e-cd2d80d5361d"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("a0925e47-3b69-41f3-8e06-3528e9ecb1ee"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashBoxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("cc803d87-1a88-427b-9cae-a2228ca6b2b1"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("49d6ad06-225b-45e0-bbce-06496b4a14b1"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("bb30094b-c15b-46aa-b172-f9e6ee67ed68"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("d242744b-e228-4642-b93b-c40c0163182a"));

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
        }
    }
}
