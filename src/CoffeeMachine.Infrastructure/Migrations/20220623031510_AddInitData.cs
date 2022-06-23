using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Капучино", 600 },
                    { 2, "Латте", 850 },
                    { 3, "Американо", 900 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 3);
        }
    }
}
