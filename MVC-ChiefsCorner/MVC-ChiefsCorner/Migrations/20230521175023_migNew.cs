using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Id",
                table: "MenuExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8750f3fc-b411-4be3-a8fe-698609de4933", "21951edd-f4ad-4901-955c-2c99e0b88c2a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7267364-ec7e-4a6e-921e-dd370d07f052", "916bb410-365e-416e-a302-b2233d82e2af", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8750f3fc-b411-4be3-a8fe-698609de4933");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7267364-ec7e-4a6e-921e-dd370d07f052");

            migrationBuilder.DropColumn(
                name: "Id",
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
    }
}
