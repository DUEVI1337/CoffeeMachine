using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoffeeMachine.Infrastructure.Migrations
{
    public partial class AddBanknoteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanknoteCashboxes",
                columns: table => new
                {
                    BanknoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Denomination = table.Column<int>(type: "integer", nullable: false),
                    CountBanknote = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknoteCashboxes", x => x.BanknoteId);
                });

            migrationBuilder.InsertData(
                table: "BanknoteCashboxes",
                columns: new[] { "BanknoteId", "CountBanknote", "Denomination" },
                values: new object[,]
                {
                    { 1, 20, 500 },
                    { 2, 15, 1000 },
                    { 3, 10, 2000 },
                    { 4, 5, 5000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknoteCashboxes");
        }
    }
}
