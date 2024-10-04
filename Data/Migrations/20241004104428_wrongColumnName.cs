using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class wrongColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7782529-8675-4e6f-9e1f-784a33f6e263");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd5249cc-7181-4a8f-8f7b-9dfe3af54ad2");

            migrationBuilder.RenameColumn(
                name: "Player2NickName",
                table: "Games",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "Player1NickName",
                table: "Games",
                newName: "Player1Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8026a3d4-7d2f-46ce-8650-dfb01d570146", null, "Player", "PLAYER" },
                    { "e44ed814-c750-47a5-9925-59d30f927365", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8026a3d4-7d2f-46ce-8650-dfb01d570146");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e44ed814-c750-47a5-9925-59d30f927365");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "Games",
                newName: "Player2NickName");

            migrationBuilder.RenameColumn(
                name: "Player1Id",
                table: "Games",
                newName: "Player1NickName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e7782529-8675-4e6f-9e1f-784a33f6e263", null, "Admin", "ADMIN" },
                    { "fd5249cc-7181-4a8f-8f7b-9dfe3af54ad2", null, "Player", "PLAYER" }
                });
        }
    }
}
