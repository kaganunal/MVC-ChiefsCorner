using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ChiefsCorner.Migrations
{
    public partial class migNew6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eca060f-d3c6-44da-9523-421a786d43d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f90a771-7af1-49dd-bd82-8e9f6a3bfa54");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ef60e7e-4ee5-471b-9a75-cda20a612a25", "b6e23571-9ac9-4ea7-bb52-a095e40c9e1a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dccac0bb-b93e-42fb-a554-3dac9fe9bea4", "d084b189-c7f4-4559-985c-9a738ed9870b", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ef60e7e-4ee5-471b-9a75-cda20a612a25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dccac0bb-b93e-42fb-a554-3dac9fe9bea4");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3eca060f-d3c6-44da-9523-421a786d43d7", "c5b706c6-5da4-4cc9-833f-155d0e36aba5", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f90a771-7af1-49dd-bd82-8e9f6a3bfa54", "1ab60661-17db-42f0-9d20-ae31af1045e5", "Admin", "ADMIN" });
        }
    }
}
