using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionVideojuegoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Videojuegos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videojuegos_UserId",
                table: "Videojuegos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuegos_AspNetUsers_UserId",
                table: "Videojuegos");

            migrationBuilder.DropIndex(
                name: "IX_Videojuegos_UserId",
                table: "Videojuegos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Videojuegos");
        }
    }
}
