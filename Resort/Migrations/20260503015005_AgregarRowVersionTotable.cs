using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRowVersionTotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Villas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6958), new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6960), new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6959) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6961), new DateTime(2026, 5, 2, 22, 50, 5, 182, DateTimeKind.Local).AddTicks(6961) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1288), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1271) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1290), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1289) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1291), new DateTime(2026, 5, 2, 21, 41, 26, 144, DateTimeKind.Local).AddTicks(1291) });
        }
    }
}
