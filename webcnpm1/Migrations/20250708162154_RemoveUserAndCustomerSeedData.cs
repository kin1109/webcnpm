using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webcnpm1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserAndCustomerSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__repair_or__assig__571DF1D5",
                table: "repair_orders");

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK__repair_or__assig__571DF1D5",
                table: "repair_orders",
                column: "assigned_to",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__repair_or__assig__571DF1D5",
                table: "repair_orders");

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "created_at", "email", "full_name", "phone" },
                values: new object[] { 1, new DateTime(2025, 7, 8, 15, 52, 55, 178, DateTimeKind.Utc).AddTicks(5895), "N/A", "Khách vãng lai", "N/A" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "Email", "full_name", "password", "role", "username" },
                values: new object[] { 1, new DateTime(2025, 7, 8, 15, 52, 55, 301, DateTimeKind.Utc).AddTicks(8634), "admin@example.com", "Admin", "$2a$11$NxoWj4NA9kc7cGHzmVuRS.qs/3gY/zn7ssQ/zeq13TCKzzutaEHKi", "Quản lý", "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK__repair_or__assig__571DF1D5",
                table: "repair_orders",
                column: "assigned_to",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
