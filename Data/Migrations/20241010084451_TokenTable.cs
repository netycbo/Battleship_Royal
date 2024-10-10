using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class TokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bab030e-42d4-4bf8-9f76-43c61366ee8c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b758fd2e-24f0-4c86-983c-f07ad9641d30");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TokenUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b65495-9fe0-49d6-8320-a27a0e4ef4e5", null, "Admin", "ADMIN" },
                    { "9f15b724-de98-4fca-a8ab-9cedc6a446cb", null, "Player", "PLAYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TokenUserId",
                table: "AspNetUsers",
                column: "TokenUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tokens_TokenUserId",
                table: "AspNetUsers",
                column: "TokenUserId",
                principalTable: "Tokens",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tokens_TokenUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TokenUserId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b65495-9fe0-49d6-8320-a27a0e4ef4e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f15b724-de98-4fca-a8ab-9cedc6a446cb");

            migrationBuilder.DropColumn(
                name: "TokenUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bab030e-42d4-4bf8-9f76-43c61366ee8c", null, "Player", "PLAYER" },
                    { "b758fd2e-24f0-4c86-983c-f07ad9641d30", null, "Admin", "ADMIN" }
                });
        }
    }
}
