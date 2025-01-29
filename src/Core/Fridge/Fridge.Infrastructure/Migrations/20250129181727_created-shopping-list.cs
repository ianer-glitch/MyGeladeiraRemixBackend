using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createdshoppinglist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingListId",
                table: "FridgeItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_ShoppingListId",
                table: "FridgeItems",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_FridgeItems_ShoppingList_ShoppingListId",
                table: "FridgeItems",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FridgeItems_ShoppingList_ShoppingListId",
                table: "FridgeItems");

            migrationBuilder.DropTable(
                name: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_FridgeItems_ShoppingListId",
                table: "FridgeItems");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "FridgeItems");
        }
    }
}
