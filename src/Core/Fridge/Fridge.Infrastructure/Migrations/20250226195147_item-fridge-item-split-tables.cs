using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class itemfridgeitemsplittables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FridgeItems_Items_Id",
                table: "FridgeItems");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Items",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FridgeItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "FridgeItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "FridgeItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Inclusion",
                table: "FridgeItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FridgeItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "FridgeItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MinimunQuantity",
                table: "FridgeItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "FridgeItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FridgeItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FridgeItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInclusionId",
                table: "FridgeItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserModifiedId",
                table: "FridgeItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "FridgeItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_ItemId",
                table: "FridgeItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FridgeItems_Items_ItemId",
                table: "FridgeItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FridgeItems_Items_ItemId",
                table: "FridgeItems");

            migrationBuilder.DropIndex(
                name: "IX_FridgeItems_ItemId",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "IconName",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Inclusion",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "MinimunQuantity",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "UserInclusionId",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "FridgeItems");

            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "Items",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddForeignKey(
                name: "FK_FridgeItems_Items_Id",
                table: "FridgeItems",
                column: "Id",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
