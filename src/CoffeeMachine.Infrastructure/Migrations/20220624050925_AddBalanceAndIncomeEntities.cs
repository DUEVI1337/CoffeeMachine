using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddBalanceAndIncomeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
