using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adchangetimetoexpiretodays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToExpire",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TimeToExpire",
                table: "FridgeItems");

            migrationBuilder.AddColumn<int>(
                name: "TimeToExpireInDays",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeToExpireInDays",
                table: "FridgeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToExpireInDays",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TimeToExpireInDays",
                table: "FridgeItems");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeToExpire",
                table: "Items",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeToExpire",
                table: "FridgeItems",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
