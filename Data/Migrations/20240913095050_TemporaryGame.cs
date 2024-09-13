using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemporaryGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporaryGames",
                columns: table => new
                {
                    Player1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Player2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSpeedGame = table.Column<bool>(type: "bit", nullable: false),
                    Timer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TemporaryGames_AspNetUsers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TemporaryGames_AspNetUsers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryGames_Player1Id",
                table: "TemporaryGames",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryGames_Player2Id",
                table: "TemporaryGames",
                column: "Player2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryGames");
        }
    }
}
