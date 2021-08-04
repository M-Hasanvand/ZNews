using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZNews.Persistence.Migrations
{
    public partial class AddIsActiveForTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 2, 5, 13, 26, 28, 360, DateTimeKind.Local).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 2, 5, 13, 26, 28, 387, DateTimeKind.Local).AddTicks(4242));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tags");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 2, 4, 23, 12, 42, 62, DateTimeKind.Local).AddTicks(1412));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 2, 4, 23, 12, 42, 73, DateTimeKind.Local).AddTicks(1681));
        }
    }
}
