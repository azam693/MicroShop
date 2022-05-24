using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    public partial class Updated_Product_Added_Combination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CombinationOptionIds = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCombinations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombinationOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CombinationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinationOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombinationOptions_Combinations_CombinationId",
                        column: x => x.CombinationId,
                        principalTable: "Combinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombinationOptions_CombinationId",
                table: "CombinationOptions",
                column: "CombinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombinations_ProductId",
                table: "ProductCombinations",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinationOptions");

            migrationBuilder.DropTable(
                name: "ProductCombinations");

            migrationBuilder.DropTable(
                name: "Combinations");
        }
    }
}
