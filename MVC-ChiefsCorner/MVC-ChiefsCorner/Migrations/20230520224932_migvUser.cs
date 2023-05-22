using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migvUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06a1c0b7-78fa-4200-ae15-02ca838c6e94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "591a1b58-847e-4379-b5fa-87698c55e2e5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05ca57aa-eaed-40b3-a64d-092429bab38b", "2e316ff2-2fa1-493f-9c39-fc2b9d5d71fa", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "943b826c-c1e6-4e6a-ae3f-49005da7d645", "bcdf1621-e4ce-48ad-abaf-3fc3807bc690", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05ca57aa-eaed-40b3-a64d-092429bab38b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "943b826c-c1e6-4e6a-ae3f-49005da7d645");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06a1c0b7-78fa-4200-ae15-02ca838c6e94", "4a833c83-6c77-491c-be0e-48768fd453e7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "591a1b58-847e-4379-b5fa-87698c55e2e5", "b149c77a-036f-4dfe-be07-7c1083b19584", "Admin", "ADMIN" });
        }
    }
}
