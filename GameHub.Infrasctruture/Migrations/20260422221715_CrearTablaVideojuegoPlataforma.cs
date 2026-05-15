using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaVideojuegoPlataforma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideojuegoPlataformas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VideojuegoId = table.Column<int>(type: "integer", nullable: false),
                    PlataformaId = table.Column<int>(type: "integer", nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoPlataformas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataformas_Plataformas_PlataformaId",
                        column: x => x.PlataformaId,
                        principalTable: "Plataformas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataformas_Videojuegos_VideojuegoId",
                        column: x => x.VideojuegoId,
                        principalTable: "Videojuegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformas_PlataformaId",
                table: "VideojuegoPlataformas",
                column: "PlataformaId");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformas_VideojuegoId",
                table: "VideojuegoPlataformas",
                column: "VideojuegoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideojuegoPlataformas");
        }
    }
}
