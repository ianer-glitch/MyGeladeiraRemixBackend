using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Statistic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialstatistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFoodWasteIndexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<float>(type: "real", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoodWasteIndexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpiredStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemWeight = table.Column<float>(type: "real", nullable: false),
                    StatisticId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inclusion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserInclusionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserModifiedId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpiredStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpiredStatistics_Statistics_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredStatistics_StatisticId",
                table: "ExpiredStatistics",
                column: "StatisticId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpiredStatistics");

            migrationBuilder.DropTable(
                name: "UserFoodWasteIndexes");

            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
