using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMenus_Orders_OrderId1",
                table: "OrderMenus");

            migrationBuilder.DropIndex(
                name: "IX_OrderMenus_OrderId1",
                table: "OrderMenus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de7e4131-985e-4daf-834c-03fd0f1b4486");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f43b0f4c-5102-4e67-9ffa-6c4faa146b0e");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderMenus");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3eca060f-d3c6-44da-9523-421a786d43d7", "c5b706c6-5da4-4cc9-833f-155d0e36aba5", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f90a771-7af1-49dd-bd82-8e9f6a3bfa54", "1ab60661-17db-42f0-9d20-ae31af1045e5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eca060f-d3c6-44da-9523-421a786d43d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f90a771-7af1-49dd-bd82-8e9f6a3bfa54");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "OrderMenus",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de7e4131-985e-4daf-834c-03fd0f1b4486", "bc515920-cb14-490b-8fbd-4c6e1a96bbef", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f43b0f4c-5102-4e67-9ffa-6c4faa146b0e", "672553c6-dd79-4fb0-b78b-7fca2344cbc3", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenus_OrderId1",
                table: "OrderMenus",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMenus_Orders_OrderId1",
                table: "OrderMenus",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
