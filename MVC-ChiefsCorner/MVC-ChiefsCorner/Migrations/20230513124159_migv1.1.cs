using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01b77170-cbd2-4540-840c-ab5f7dfd75ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edaeafe6-7bec-4d39-b6b3-e992e51f1734");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60ef6a71-49e8-4d74-b9ef-faaa9c1d0b83", "de5c5216-23c7-4d37-bbc8-e52fdf6c4195", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "746c3afd-52f4-4e69-bdc7-7f31cb43db19", "2b0942af-61d6-4a81-b593-505c465207d0", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60ef6a71-49e8-4d74-b9ef-faaa9c1d0b83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "746c3afd-52f4-4e69-bdc7-7f31cb43db19");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Menus");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01b77170-cbd2-4540-840c-ab5f7dfd75ea", "1ae8f8c3-6710-4eb4-b0a0-79d27a30cffb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "edaeafe6-7bec-4d39-b6b3-e992e51f1734", "696c1a2a-e48d-4574-960c-b31622f1699c", "Customer", "CUSTOMER" });
        }
    }
}
