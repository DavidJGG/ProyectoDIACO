using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDIACO.Migrations
{
    public partial class comerciosSucursales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comercio",
                columns: table => new
                {
                    ComercioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comercio", x => x.ComercioId);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    SucursalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    ComercioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.SucursalId);
                    table.ForeignKey(
                        name: "FK_Sucursal_Comercio_ComercioId",
                        column: x => x.ComercioId,
                        principalTable: "Comercio",
                        principalColumn: "ComercioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sucursal_Ubicacion_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Ubicacion",
                        principalColumn: "UbicacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_ComercioId",
                table: "Sucursal",
                column: "ComercioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_MunicipioId",
                table: "Sucursal",
                column: "MunicipioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Comercio");
        }
    }
}
