using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDIACO.Migrations
{
    public partial class ubicaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    UbicacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    SububicacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.UbicacionId);
                });

            migrationBuilder.InsertData(
                table: "Ubicacion",
                columns: new[] { "UbicacionId", "Nombre", "SububicacionId", "Tipo" },
                values: new object[,]
                {
                    { 1, "Suroccidente", null, 1 },
                    { 2, "Metropolitana", null, 1 },
                    { 3, "Noroccidente", null, 1 },
                    { 4, "Central", null, 1 },
                    { 5, "Verapaz", null, 1 },
                    { 6, "Nororiente", null, 1 },
                    { 7, "Suroriente", null, 1 },
                    { 8, "Petén", null, 1 },
                    { 9, "Quetzaltenango", 1, 2 },
                    { 10, "Guatemala", 2, 2 },
                    { 11, "Huehuetenango", 3, 2 },
                    { 12, "Chimaltenango", 4, 2 },
                    { 13, "Alta Verapaz", 5, 2 },
                    { 14, "Chiquimula", 6, 2 },
                    { 15, "Jutiapa", 7, 2 },
                    { 16, "Petén", 8, 2 },
                    { 17, "Quetzaltenango", 9, 3 },
                    { 18, "Guatemala", 10, 3 },
                    { 19, "Huehuetenango", 11, 3 },
                    { 20, "Chimaltenango", 12, 3 },
                    { 21, "Coban", 13, 3 },
                    { 22, "Chiquimula", 14, 3 },
                    { 23, "Jutiapa", 15, 3 },
                    { 24, "Flores", 16, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicacion");
        }
    }
}
