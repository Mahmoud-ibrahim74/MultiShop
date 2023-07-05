using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultiShop.Migrations
{
    /// <inheritdoc />
    public partial class roles3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51ee8d3c-9d76-4efe-9acb-a9364e96e560");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e54f310d-0357-45af-ab97-024ea83d03b5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19f33065-47ab-415e-a95f-3f9614be46d4", "918345cb-2be8-4908-86e3-3aeb7f4caa3a", "User", "User" },
                    { "9310fbf1-475e-45fb-a7df-a08ab933e412", "21565e8d-8be6-4800-9ce2-7aa1362ba2ae", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19f33065-47ab-415e-a95f-3f9614be46d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9310fbf1-475e-45fb-a7df-a08ab933e412");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51ee8d3c-9d76-4efe-9acb-a9364e96e560", "2bd3f595-eabc-430d-92ec-b13b7de3b78a", null, "User" },
                    { "e54f310d-0357-45af-ab97-024ea83d03b5", "e70003bb-dd07-4679-80e2-8571872bc00f", null, "Admin" }
                });
        }
    }
}
