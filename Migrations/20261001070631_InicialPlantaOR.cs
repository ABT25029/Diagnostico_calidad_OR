using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InicialPlantaOR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialPlanta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ph = table.Column<double>(type: "REAL", nullable: false),
                    Conductividad = table.Column<double>(type: "REAL", nullable: false),
                    FlujoPermeado = table.Column<double>(type: "REAL", nullable: false),
                    FlujoRechazo = table.Column<double>(type: "REAL", nullable: false),
                    PorcentajeRecuperacion = table.Column<double>(type: "REAL", nullable: false),
                    CumpleParametros = table.Column<bool>(type: "INTEGER", nullable: false),
                    EstadoGeneral = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPlanta", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialPlanta");
        }
    }
}
