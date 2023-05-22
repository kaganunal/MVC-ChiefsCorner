using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69fea79c-33ba-4824-a51e-98f536fe976c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1dc5f25-bea3-4e07-8f9e-f20f9cf3271e");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MenuExtras");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c6a2311-9d70-4872-aacf-085b5f01db1b", "1d10f217-bb78-4533-af6d-36b95def1865", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2732f35-ad66-439e-b4d0-e2c93f55a77f", "4748adb0-016f-4227-989e-f0807db56e49", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6a2311-9d70-4872-aacf-085b5f01db1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2732f35-ad66-439e-b4d0-e2c93f55a77f");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MenuExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69fea79c-33ba-4824-a51e-98f536fe976c", "97b013cc-14bf-440e-95e0-f2d62dbf71dd", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1dc5f25-bea3-4e07-8f9e-f20f9cf3271e", "2a540484-b8f1-4899-b588-3f083d23be5a", "Admin", "ADMIN" });
        }
    }
}
