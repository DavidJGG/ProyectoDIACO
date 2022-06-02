using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDIACO.Migrations
{
    public partial class estadoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Usuario",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuario");
        }
    }
}
