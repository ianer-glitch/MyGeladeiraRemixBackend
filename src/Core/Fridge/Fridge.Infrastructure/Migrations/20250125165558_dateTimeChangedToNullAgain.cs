using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dateTimeChangedToNullAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultQuantity",
                table: "Items",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Items",
                newName: "DefaultQuantity");
        }
    }
}
