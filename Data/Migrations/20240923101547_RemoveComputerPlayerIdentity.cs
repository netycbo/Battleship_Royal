using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship_Royal.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComputerPlayerIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "ComputerPlayers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ComputerPlayers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "ComputerPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ComputerPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "ComputerPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "ComputerPlayers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "ComputerPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "ComputerPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ComputerPlayers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
