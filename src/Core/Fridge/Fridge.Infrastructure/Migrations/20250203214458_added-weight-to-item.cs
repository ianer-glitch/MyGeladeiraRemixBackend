using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedweighttoitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Items",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Items");
        }
    }
}
