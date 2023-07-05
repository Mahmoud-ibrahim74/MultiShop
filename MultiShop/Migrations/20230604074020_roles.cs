using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultiShop.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51ee8d3c-9d76-4efe-9acb-a9364e96e560", "2bd3f595-eabc-430d-92ec-b13b7de3b78a", null, "User" },
                    { "e54f310d-0357-45af-ab97-024ea83d03b5", "e70003bb-dd07-4679-80e2-8571872bc00f", null, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51ee8d3c-9d76-4efe-9acb-a9364e96e560");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e54f310d-0357-45af-ab97-024ea83d03b5");
        }
    }
}
