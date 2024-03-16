using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektMVCdotnet8.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "UserEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "UserEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "UserEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "UserEntity",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "UserEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "UserEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserEntity",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserEntity");
        }
    }
}
