using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migv34 : Migration
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
                keyValue: "463edc46-54bd-4e89-9051-c854cd63ba2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8b76043-0d8a-4b8e-b45d-ef007795de79");

            migrationBuilder.DropColumn(
                name: "OrderMenuId",
                table: "MenuExtras");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Extras",
                type: "nvarchar(max)",
                nullable: true);

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
                values: new object[] { "5a671013-2dd2-4b3f-832a-69acdcdb2245", "e39abe42-7c81-444f-8ae6-7dc996c66562", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a4f4cb08-92ce-4862-bac7-aa95a034b925", "a55f2557-7cc1-418f-85d9-45687d705aa1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtraOrderMenu_MenuExtrasMenuId_MenuExtrasExtraId",
                table: "MenuExtraOrderMenu",
                columns: new[] { "MenuExtrasMenuId", "MenuExtrasExtraId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuExtraOrderMenu");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a671013-2dd2-4b3f-832a-69acdcdb2245");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4f4cb08-92ce-4862-bac7-aa95a034b925");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Extras");

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
    }
}
