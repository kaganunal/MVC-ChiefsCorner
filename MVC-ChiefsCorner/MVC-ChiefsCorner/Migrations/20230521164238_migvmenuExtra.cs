using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migvmenuExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ed01496-c8de-44aa-8725-6b877224fb66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6a8a489-30d7-4f5b-bed9-e93d142885ed");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce26019f-ed40-4653-ba71-9ca6b6fd5fdc", "26ffe1f1-92a7-4cc4-8ae7-e9c4a9bee551", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc27a2c3-a9c9-47a5-8431-650daf323122", "b58fcd9c-c1f7-435f-9283-fb394b0bd267", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce26019f-ed40-4653-ba71-9ca6b6fd5fdc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc27a2c3-a9c9-47a5-8431-650daf323122");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Menus");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ed01496-c8de-44aa-8725-6b877224fb66", "baa91f79-097b-440a-b4a1-c054ad6a0fd3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6a8a489-30d7-4f5b-bed9-e93d142885ed", "a2a5f132-29c6-4212-be36-532c25ed327e", "Admin", "ADMIN" });
        }
    }
}
