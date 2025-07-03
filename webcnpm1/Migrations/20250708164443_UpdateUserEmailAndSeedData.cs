using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webcnpm1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEmailAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "password", "role", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 8, 16, 44, 40, 833, DateTimeKind.Utc).AddTicks(4359), "admin@repaircenter.com", "Administrator", "$2a$11$Gd6xthQzclV.JrBCFD6i8utdcfmk5fOwBO2yn/GaxDkaLoSwv4cG6", "Quản lý", "admin" },
                    { 2, new DateTime(2025, 7, 8, 16, 44, 40, 954, DateTimeKind.Utc).AddTicks(9065), "tech@repaircenter.com", "Kỹ thuật viên", "$2a$11$Xxrv8KfKCjH0o6Wv9NSODuMkPMHNeUiyffRPmVUwzdaanorxn5TKK", "Nhân viên kỹ thuật", "technician" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E6164",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__users__AB6E6164",
                table: "users");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
