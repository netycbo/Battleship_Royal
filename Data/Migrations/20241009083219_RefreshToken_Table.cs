using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8026a3d4-7d2f-46ce-8650-dfb01d570146");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e44ed814-c750-47a5-9925-59d30f927365");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "ComputerPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "ComputerPlayers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DifficultyLevel",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "DifficultyLevel",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8026a3d4-7d2f-46ce-8650-dfb01d570146", null, "Player", "PLAYER" },
                    { "e44ed814-c750-47a5-9925-59d30f927365", null, "Admin", "ADMIN" }
                });
        }
    }
}
