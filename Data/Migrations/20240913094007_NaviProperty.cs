using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class NaviProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserGame",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGame", x => new { x.GamesGameId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_AspNetUsers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGame_PlayersId",
                table: "ApplicationUserGame",
                column: "PlayersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGame");
        }
    }
}
