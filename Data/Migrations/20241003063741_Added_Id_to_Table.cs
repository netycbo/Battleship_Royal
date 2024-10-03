using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Id_to_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TemporaryGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TemporaryGames");
        }
    }
}
