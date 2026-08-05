using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "CreatedDate", "Description", "ImageUrl", "IsActive", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Home Appliances", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Refridgerator", "www.example.com", true, "Fridge", 80000m, 2 },
                    { 2, "Electronics", new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobile Phone", "www.something.com", true, "iPhone", 1000m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
        }
    }
}
