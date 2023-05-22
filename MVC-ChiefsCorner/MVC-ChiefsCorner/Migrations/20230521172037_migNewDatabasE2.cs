using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNewDatabasE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuExtras_OrderMenus_OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.DropIndex(
                name: "IX_MenuExtras_OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65bab0a6-2c88-4ee0-b202-6c92da4e6025");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6702a84a-f07a-41f5-b646-18f05785a232");

            migrationBuilder.DropColumn(
                name: "OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23afd0cc-2449-4c5b-9823-4aa5c7e955bc", "c0b98d00-3a8b-41cb-9a75-fc5773922c3f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a8c671d-328e-4181-961e-8bc1e0b21da5", "dbf8443c-4454-4cd6-a01c-4a2059486ff7", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23afd0cc-2449-4c5b-9823-4aa5c7e955bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a8c671d-328e-4181-961e-8bc1e0b21da5");

            migrationBuilder.AddColumn<int>(
                name: "OrderMenuId",
                table: "MenuExtras",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65bab0a6-2c88-4ee0-b202-6c92da4e6025", "00f615cb-d5be-49bc-b25d-596173509707", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6702a84a-f07a-41f5-b646-18f05785a232", "bd629a47-5f00-44ef-a184-480b64fbe050", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtras_OrderMenuId",
                table: "MenuExtras",
                column: "OrderMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuExtras_OrderMenus_OrderMenuId",
                table: "MenuExtras",
                column: "OrderMenuId",
                principalTable: "OrderMenus",
                principalColumn: "Id");
        }
    }
}
