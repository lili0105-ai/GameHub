using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntegraDesarrolladorEnIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_Desarrolladores_DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.DropTable(
                name: "Desarrolladores");

            migrationBuilder.DropIndex(
                name: "IX_Videojuegos_UserId",
                table: "Videojuegos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Videojuegos");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "AspNetUsers",
                newName: "NombreDesarrollador");

            migrationBuilder.AlterColumn<string>(
                name: "DesarrolladorId",
                table: "Videojuegos",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaFundacion",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaRegistro",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_AspNetUsers_DesarrolladorId",
                table: "Videojuegos",
                column: "DesarrolladorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_AspNetUsers_DesarrolladorId",
                table: "Videojuegos");

            migrationBuilder.DropColumn(
                name: "FechaFundacion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NombreDesarrollador",
                table: "AspNetUsers",
                newName: "NombreCompleto");

            migrationBuilder.AlterColumn<int>(
                name: "DesarrolladorId",
                table: "Videojuegos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Videojuegos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Desarrolladores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaFundacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false),
                    NombreDesarrollador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desarrolladores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videojuegos_UserId",
                table: "Videojuegos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Desarrolladores_NombreDesarrollador",
                table: "Desarrolladores",
                column: "NombreDesarrollador",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_Desarrolladores_DesarrolladorId",
                table: "Videojuegos",
                column: "DesarrolladorId",
                principalTable: "Desarrolladores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
