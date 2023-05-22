using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuExtras");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6a2311-9d70-4872-aacf-085b5f01db1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2732f35-ad66-439e-b4d0-e2c93f55a77f");

            migrationBuilder.DropColumn(
                name: "MenuExtraId",
                table: "OrderMenus");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MenuExtraId",
                table: "OrderMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuExtras",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ExtraId = table.Column<int>(type: "int", nullable: false),
                    OrderMenuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuExtras", x => new { x.MenuId, x.ExtraId });
                    table.ForeignKey(
                        name: "FK_MenuExtras_Extras_ExtraId",
                        column: x => x.ExtraId,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuExtras_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuExtras_OrderMenus_OrderMenuId",
                        column: x => x.OrderMenuId,
                        principalTable: "OrderMenus",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c6a2311-9d70-4872-aacf-085b5f01db1b", "1d10f217-bb78-4533-af6d-36b95def1865", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2732f35-ad66-439e-b4d0-e2c93f55a77f", "4748adb0-016f-4227-989e-f0807db56e49", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtras_ExtraId",
                table: "MenuExtras",
                column: "ExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuExtras_OrderMenuId",
                table: "MenuExtras",
                column: "OrderMenuId");
        }
    }
}
