using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e7782529-8675-4e6f-9e1f-784a33f6e263", null, "Admin", "ADMIN" },
                    { "fd5249cc-7181-4a8f-8f7b-9dfe3af54ad2", null, "Player", "PLAYER" }
                });

            migrationBuilder.InsertData(
                table: "ComputerPlayers",
                columns: new[] { "Id", "NickName" },
                values: new object[] { 1, "Red October" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7782529-8675-4e6f-9e1f-784a33f6e263");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd5249cc-7181-4a8f-8f7b-9dfe3af54ad2");

            migrationBuilder.DeleteData(
                table: "ComputerPlayers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
