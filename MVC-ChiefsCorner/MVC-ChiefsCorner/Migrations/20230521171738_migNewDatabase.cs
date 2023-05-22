using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuExtraOrderMenu");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce26019f-ed40-4653-ba71-9ca6b6fd5fdc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc27a2c3-a9c9-47a5-8431-650daf323122");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MenuExtras");

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
                keyValue: "65bab0a6-2c88-4ee0-b202-6c92da4e6025");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6702a84a-f07a-41f5-b646-18f05785a232");

            migrationBuilder.DropColumn(
                name: "OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MenuExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MenuExtraOrderMenu",
                columns: table => new
                {
                    OrderMenusId = table.Column<int>(type: "int", nullable: false),
                    MenuExtrasMenuId = table.Column<int>(type: "int", nullable: false),
                    MenuExtrasExtraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuExtraOrderMenu", x => new { x.OrderMenusId, x.MenuExtrasMenuId, x.MenuExtrasExtraId });
                    table.ForeignKey(
                        name: "FK_MenuExtraOrderMenu_MenuExtras_MenuExtrasMenuId_MenuExtrasExtraId",
                        columns: x => new { x.MenuExtrasMenuId, x.MenuExtrasExtraId },
                        principalTable: "MenuExtras",
                        principalColumns: new[] { "MenuId", "ExtraId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuExtraOrderMenu_OrderMenus_OrderMenusId",
                        column: x => x.OrderMenusId,
                        principalTable: "OrderMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce26019f-ed40-4653-ba71-9ca6b6fd5fdc", "26ffe1f1-92a7-4cc4-8ae7-e9c4a9bee551", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc27a2c3-a9c9-47a5-8431-650daf323122", "b58fcd9c-c1f7-435f-9283-fb394b0bd267", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtraOrderMenu_MenuExtrasMenuId_MenuExtrasExtraId",
                table: "MenuExtraOrderMenu",
                columns: new[] { "MenuExtrasMenuId", "MenuExtrasExtraId" });
        }
    }
}
