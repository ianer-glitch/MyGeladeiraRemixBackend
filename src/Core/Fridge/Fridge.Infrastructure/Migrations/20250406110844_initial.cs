using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FridgeSchema");

            migrationBuilder.CreateTable(
                name: "Fridges",
                schema: "FridgeSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "FridgeSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimunQuantity = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TimeToExpireInDays = table.Column<int>(type: "int", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                schema: "FridgeSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FridgeItems",
                schema: "FridgeSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FridgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeToExpireInDays = table.Column<int>(type: "int", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimunQuantity = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FridgeItems_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalSchema: "FridgeSchema",
                        principalTable: "Fridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "FridgeSchema",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeItems_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalSchema: "FridgeSchema",
                        principalTable: "ShoppingList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_FridgeId",
                schema: "FridgeSchema",
                table: "FridgeItems",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_ItemId",
                schema: "FridgeSchema",
                table: "FridgeItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_ShoppingListId",
                schema: "FridgeSchema",
                table: "FridgeItems",
                column: "ShoppingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeItems",
                schema: "FridgeSchema");

            migrationBuilder.DropTable(
                name: "Fridges",
                schema: "FridgeSchema");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "FridgeSchema");

            migrationBuilder.DropTable(
                name: "ShoppingList",
                schema: "FridgeSchema");
        }
    }
}
