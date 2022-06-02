using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDIACO.Migrations
{
    public partial class agregandoEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Sucursal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Comercio",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Comercio");
        }
    }
}
