using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "AspNetUsers",
                newName: "NickName");

            migrationBuilder.AddColumn<int>(
                name: "ComputerPlayerId",
                table: "TemporaryGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComputerPlayerId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComputerPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerPlayers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryGames_ComputerPlayerId",
                table: "TemporaryGames",
                column: "ComputerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ComputerPlayerId",
                table: "Games",
                column: "ComputerPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ComputerPlayers_ComputerPlayerId",
                table: "Games",
                column: "ComputerPlayerId",
                principalTable: "ComputerPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryGames_ComputerPlayers_ComputerPlayerId",
                table: "TemporaryGames",
                column: "ComputerPlayerId",
                principalTable: "ComputerPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ComputerPlayers_ComputerPlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryGames_ComputerPlayers_ComputerPlayerId",
                table: "TemporaryGames");

            migrationBuilder.DropTable(
                name: "ComputerPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryGames_ComputerPlayerId",
                table: "TemporaryGames");

            migrationBuilder.DropIndex(
                name: "IX_Games_ComputerPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ComputerPlayerId",
                table: "TemporaryGames");

            migrationBuilder.DropColumn(
                name: "ComputerPlayerId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "AspNetUsers",
                newName: "Nickname");
        }
    }
}
