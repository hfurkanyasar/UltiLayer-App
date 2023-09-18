using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 22, 30, 306, DateTimeKind.Local).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 22, 30, 306, DateTimeKind.Local).AddTicks(1012));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 22, 30, 306, DateTimeKind.Local).AddTicks(1013));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 22, 30, 306, DateTimeKind.Local).AddTicks(1014));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 22, 30, 306, DateTimeKind.Local).AddTicks(1014));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 14, 37, 266, DateTimeKind.Local).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 14, 37, 266, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 14, 37, 266, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 14, 37, 266, DateTimeKind.Local).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 16, 16, 14, 37, 266, DateTimeKind.Local).AddTicks(8084));
        }
    }
}
