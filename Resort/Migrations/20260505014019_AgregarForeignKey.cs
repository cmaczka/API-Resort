using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Migrations
{
    /// <inheritdoc />
    public partial class AgregarForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1468), new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1453) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1470), new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1469) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1472), new DateTime(2026, 5, 4, 22, 40, 19, 715, DateTimeKind.Local).AddTicks(1471) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9888), new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9871) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9890), new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9890) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9892), new DateTime(2026, 5, 4, 22, 4, 0, 515, DateTimeKind.Local).AddTicks(9892) });
        }
    }
}
