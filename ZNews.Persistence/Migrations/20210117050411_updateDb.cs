using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZNews.Persistence.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 1, 17, 8, 34, 9, 973, DateTimeKind.Local).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 1, 17, 8, 34, 9, 991, DateTimeKind.Local).AddTicks(4057));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 1, 7, 11, 20, 2, 978, DateTimeKind.Local).AddTicks(3465));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 1, 7, 11, 20, 3, 15, DateTimeKind.Local).AddTicks(1737));
        }
    }
}
