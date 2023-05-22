using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migv20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3567b416-8adf-40fe-adae-7d5126f33914");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7443b305-60dd-467f-9ad4-4e17542df14c");

            migrationBuilder.AddColumn<int>(
                name: "MenuExtraId",
                table: "OrderMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderMenuId",
                table: "MenuExtras",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "463edc46-54bd-4e89-9051-c854cd63ba2c", "5a34cd0f-3eb1-445f-acf0-029d783b74a9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8b76043-0d8a-4b8e-b45d-ef007795de79", "5b2bec6b-821a-4db2-8b49-55bf685b17a8", "Admin", "ADMIN" });

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
                keyValue: "463edc46-54bd-4e89-9051-c854cd63ba2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8b76043-0d8a-4b8e-b45d-ef007795de79");

            migrationBuilder.DropColumn(
                name: "MenuExtraId",
                table: "OrderMenus");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderMenus");

            migrationBuilder.DropColumn(
                name: "OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3567b416-8adf-40fe-adae-7d5126f33914", "a19768ea-a5fb-4328-b13c-634c13e4ab41", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7443b305-60dd-467f-9ad4-4e17542df14c", "a147d843-e03b-4be6-994c-e56924b346e7", "Admin", "ADMIN" });
        }
    }
}
