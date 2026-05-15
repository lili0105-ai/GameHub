using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionVideojuegoGeneroDesarrollador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesarrolladorId",
                table: "Videojuegos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Videojuegos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Videojuegos_DesarrolladorId",
                table: "Videojuegos",
                column: "DesarrolladorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videojuegos_GeneroId",
                table: "Videojuegos",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_Desarrolladores_DesarrolladorId",
                table: "Videojuegos",
                column: "DesarrolladorId",
                principalTable: "Desarrolladores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_Generos_GeneroId",
                table: "Videojuegos",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_Desarrolladores_DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_Generos_GeneroId",
                table: "Videojuegos");

            migrationBuilder.DropIndex(
                name: "IX_Videojuegos_DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.DropIndex(
                name: "IX_Videojuegos_GeneroId",
                table: "Videojuegos");

            migrationBuilder.DropColumn(
                name: "DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Videojuegos");
        }
    }
}
