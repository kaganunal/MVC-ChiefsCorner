using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8750f3fc-b411-4be3-a8fe-698609de4933");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7267364-ec7e-4a6e-921e-dd370d07f052");

            migrationBuilder.AddColumn<int>(
                name: "OrderMenuId",
                table: "MenuExtras",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69fea79c-33ba-4824-a51e-98f536fe976c", "97b013cc-14bf-440e-95e0-f2d62dbf71dd", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1dc5f25-bea3-4e07-8f9e-f20f9cf3271e", "2a540484-b8f1-4899-b588-3f083d23be5a", "Admin", "ADMIN" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "69fea79c-33ba-4824-a51e-98f536fe976c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1dc5f25-bea3-4e07-8f9e-f20f9cf3271e");

            migrationBuilder.DropColumn(
                name: "OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8750f3fc-b411-4be3-a8fe-698609de4933", "21951edd-f4ad-4901-955c-2c99e0b88c2a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7267364-ec7e-4a6e-921e-dd370d07f052", "916bb410-365e-416e-a302-b2233d82e2af", "Admin", "ADMIN" });
        }
    }
}
