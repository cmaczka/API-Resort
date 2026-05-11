using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarifa = table.Column<double>(type: "float", nullable: false),
                    Ocupantes = table.Column<int>(type: "int", nullable: false),
                    MetrosCuadrados = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Amenidad de la Villa 1", "Detalle de la Villa 1", new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2383), new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2366), "https://example.com/villa1.jpg", 50.0, "Villa 1", 3, 100.0 },
                    { 2, "Amenidad de la Villa 2", "Detalle de la Villa 2", new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2385), new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2384), "https://example.com/villa2.jpg", 60.0, "Villa 2", 4, 150.0 },
                    { 3, "Amenidad de la Villa 3", "Detalle de la Villa 3", new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2386), new DateTime(2026, 5, 8, 23, 14, 31, 5, DateTimeKind.Local).AddTicks(2386), "https://example.com/villa3.jpg", 70.0, "Villa 3", 5, 200.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
