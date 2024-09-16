using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsInGame_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInGame",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInGame",
                table: "Games");
        }
    }
}
