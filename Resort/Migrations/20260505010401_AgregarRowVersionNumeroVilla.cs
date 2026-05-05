using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRowVersionNumeroVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "NumeroVillas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6829), new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6815) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6832), new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6831) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6833), new DateTime(2026, 5, 4, 21, 7, 11, 98, DateTimeKind.Local).AddTicks(6833) });
        }
    }
}
