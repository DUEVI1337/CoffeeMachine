using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddEntityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("25d7c364-da5e-473a-a64c-ee19849fba34"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("2e0f54d8-bb5d-49d4-929e-53e7de42b7fb"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("494c6473-63f0-40c3-9ace-61ea6487b5ec"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("8040df98-0a0e-493c-aa1a-881ed63d7afd"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("d0ef9196-b8c1-4650-a5b0-662a41dda7b9"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("df0c6023-58c2-4f9e-8cb6-57d3df7d95ff"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("e66c9691-7cba-4ee2-8b81-7e843512ea57"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("0a0151ed-1e06-47ca-956d-8b61e05163eb"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("56dc0c27-a56d-4fb9-a899-83d28223fffe"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("78f439f3-0804-48f4-96cf-f22c67b2a943"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.InsertData(
                table: "BanknoteCashboxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { new Guid("b25ba405-8a95-4f68-906f-6b565c83feb2"), 50, 50 },
                    { new Guid("0d83dd1e-e17a-4e98-8322-7bd0115bc298"), 40, 100 },
                    { new Guid("0e02ee51-af36-4a46-a81f-83704eb16a45"), 30, 200 },
                    { new Guid("50d91f4e-a198-4019-a45c-d43ff44ea993"), 20, 500 },
                    { new Guid("70c38472-1346-4661-942f-7f5109ffcf05"), 15, 1000 },
                    { new Guid("dcf568ce-a3a8-458c-b3c5-c597142a8890"), 10, 2000 },
                    { new Guid("5bd19c6a-e458-4658-a923-4793b8cb4415"), 5, 5000 }
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("ecc10c72-a8ca-4fed-8e87-c1be4de129f8"), "Капучино", 600 },
                    { new Guid("f5c88077-8eeb-4a88-adc2-1c474fb411af"), "Латте", 850 },
                    { new Guid("bfa64581-25f1-4bb3-950a-c463e555e4be"), "Американо", 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("0d83dd1e-e17a-4e98-8322-7bd0115bc298"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("0e02ee51-af36-4a46-a81f-83704eb16a45"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("50d91f4e-a198-4019-a45c-d43ff44ea993"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("5bd19c6a-e458-4658-a923-4793b8cb4415"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("70c38472-1346-4661-942f-7f5109ffcf05"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("b25ba405-8a95-4f68-906f-6b565c83feb2"));

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: new Guid("dcf568ce-a3a8-458c-b3c5-c597142a8890"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("bfa64581-25f1-4bb3-950a-c463e555e4be"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("ecc10c72-a8ca-4fed-8e87-c1be4de129f8"));

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: new Guid("f5c88077-8eeb-4a88-adc2-1c474fb411af"));

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
        }
    }
}
