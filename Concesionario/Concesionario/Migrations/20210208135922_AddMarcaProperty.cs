using Microsoft.EntityFrameworkCore.Migrations;

namespace Concesionario.Migrations
{
    public partial class AddMarcaProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Car");
        }
    }
}
