using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddBonusInitBanknote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 1,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 50, 50 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 2,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 40, 100 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 3,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 30, 200 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 4,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 20, 500 });

            migrationBuilder.InsertData(
                table: "BanknoteCashboxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { 5, 15, 1000 },
                    { 6, 10, 2000 },
                    { 7, 5, 5000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 1,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 20, 500 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 2,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 15, 1000 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 3,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 10, 2000 });

            migrationBuilder.UpdateData(
                table: "BanknoteCashboxes",
                keyColumn: "BanknoteId",
                keyValue: 4,
                columns: new[] { "CountBanknote", "Denomination" },
                values: new object[] { 5, 5000 });
        }
    }
}
