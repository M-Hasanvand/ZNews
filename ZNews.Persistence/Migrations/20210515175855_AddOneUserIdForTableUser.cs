using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZNews.Persistence.Migrations
{
    public partial class AddOneUserIdForTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OneUserId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 5, 15, 22, 28, 52, 901, DateTimeKind.Local).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 5, 15, 22, 28, 52, 911, DateTimeKind.Local).AddTicks(1550));

            migrationBuilder.CreateIndex(
                name: "IX_Users_OneUserId",
                table: "Users",
                column: "OneUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_OneUserId",
                table: "Users",
                column: "OneUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_OneUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OneUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OneUserId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2021, 5, 5, 2, 51, 17, 239, DateTimeKind.Local).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2021, 5, 5, 2, 51, 17, 284, DateTimeKind.Local).AddTicks(4382));
        }
    }
}
