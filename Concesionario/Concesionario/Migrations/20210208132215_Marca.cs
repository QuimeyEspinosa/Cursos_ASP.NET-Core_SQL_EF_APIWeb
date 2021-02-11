using Microsoft.EntityFrameworkCore.Migrations;

namespace Concesionario.Migrations
{
    public partial class Marca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_MarcaId",
                table: "Car",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Marca_MarcaId",
                table: "Car",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Marca_MarcaId",
                table: "Car");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropIndex(
                name: "IX_Car_MarcaId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Car");
        }
    }
}
