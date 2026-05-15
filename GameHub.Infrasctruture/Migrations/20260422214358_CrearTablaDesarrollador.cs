using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaDesarrollador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desarrolladores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreDesarrollador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    FechaFundacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desarrolladores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desarrolladores_NombreDesarrollador",
                table: "Desarrolladores",
                column: "NombreDesarrollador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desarrolladores");
        }
    }
}
