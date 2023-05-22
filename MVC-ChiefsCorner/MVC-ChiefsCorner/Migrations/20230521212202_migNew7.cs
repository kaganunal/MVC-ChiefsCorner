using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ef60e7e-4ee5-471b-9a75-cda20a612a25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dccac0bb-b93e-42fb-a554-3dac9fe9bea4");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df2ba986-0b32-4fd7-9ca9-94907ef49f94", "6657e230-46de-4172-b882-5a1cbb88f590", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f883fd5d-b90f-4c09-b028-9b3c938186c6", "ed0b719a-03d9-4d12-b6b7-f7585dc0e9f7", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df2ba986-0b32-4fd7-9ca9-94907ef49f94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f883fd5d-b90f-4c09-b028-9b3c938186c6");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ef60e7e-4ee5-471b-9a75-cda20a612a25", "b6e23571-9ac9-4ea7-bb52-a095e40c9e1a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dccac0bb-b93e-42fb-a554-3dac9fe9bea4", "d084b189-c7f4-4559-985c-9a738ed9870b", "Customer", "CUSTOMER" });
        }
    }
}
