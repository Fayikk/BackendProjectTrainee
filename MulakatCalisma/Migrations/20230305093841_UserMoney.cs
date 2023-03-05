using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MulakatCalisma.Migrations
{
    public partial class UserMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Money",
                table: "UserMoneys",
                type: "Decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Money",
                table: "UserMoneys",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(18,2)");
        }
    }
}
