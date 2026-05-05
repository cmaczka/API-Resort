using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
