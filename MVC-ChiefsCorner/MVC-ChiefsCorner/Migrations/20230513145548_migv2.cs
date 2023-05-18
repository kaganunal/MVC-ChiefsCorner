using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae9b842b-a9cd-47e3-8da3-5de17e1ecbd6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e588765b-7cb9-46f4-bc1e-65be62e87cf8");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "MenuCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06a1c0b7-78fa-4200-ae15-02ca838c6e94", "4a833c83-6c77-491c-be0e-48768fd453e7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "591a1b58-847e-4379-b5fa-87698c55e2e5", "b149c77a-036f-4dfe-be07-7c1083b19584", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06a1c0b7-78fa-4200-ae15-02ca838c6e94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "591a1b58-847e-4379-b5fa-87698c55e2e5");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "MenuCategories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae9b842b-a9cd-47e3-8da3-5de17e1ecbd6", "be1e055d-ccfa-4316-939a-8f7a54ef52aa", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e588765b-7cb9-46f4-bc1e-65be62e87cf8", "ac413a7a-3e27-40d9-a02f-f98207741165", "Customer", "CUSTOMER" });
        }
    }
}
