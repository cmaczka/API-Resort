using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort.Migrations
{
    /// <inheritdoc />
    public partial class alimentartablavilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Amenidad de la Villa 1", "Detalle de la Villa 1", new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1288), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1271), "https://example.com/villa1.jpg", 50.0, "Villa 1", 3, 100.0 },
                    { 2, "Amenidad de la Villa 2", "Detalle de la Villa 2", new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1290), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1289), "https://example.com/villa2.jpg", 60.0, "Villa 2", 4, 150.0 },
                    { 3, "Amenidad de la Villa 3", "Detalle de la Villa 3", new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1291), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1291), "https://example.com/villa3.jpg", 70.0, "Villa 3", 5, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
