using Microsoft.EntityFrameworkCore.Migrations;

namespace International_Business_Men.DAL.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    From = table.Column<string>(nullable: false),
                    To = table.Column<string>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(12, 4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => new { x.From, x.To });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates");
        }
    }
}
