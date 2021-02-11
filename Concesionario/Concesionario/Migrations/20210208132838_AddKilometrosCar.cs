using Microsoft.EntityFrameworkCore.Migrations;

namespace Concesionario.Migrations
{
    public partial class AddKilometrosCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kilometros",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kilometros",
                table: "Car");
        }
    }
}
