using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablarefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_AspNetUsers_DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.RenameColumn(
                name: "DesarrolladorId",
                table: "Videojuegos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Videojuegos_DesarrolladorId",
                table: "Videojuegos",
                newName: "IX_Videojuegos_UserId");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true),
                    Expiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UsuarioId",
                table: "RefreshTokens",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Videojuegos",
                newName: "DesarrolladorId");

            migrationBuilder.RenameIndex(
                name: "IX_Videojuegos_UserId",
                table: "Videojuegos",
                newName: "IX_Videojuegos_DesarrolladorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_AspNetUsers_DesarrolladorId",
                table: "Videojuegos",
                column: "DesarrolladorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
