using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webcnpm1.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "created_at", "email", "full_name", "phone" },
                values: new object[] { 1, new DateTime(2025, 7, 8, 15, 52, 55, 178, DateTimeKind.Utc).AddTicks(5895), "N/A", "Khách vãng lai", "N/A" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "Email", "full_name", "password", "role", "username" },
                values: new object[] { 1, new DateTime(2025, 7, 8, 15, 52, 55, 301, DateTimeKind.Utc).AddTicks(8634), "admin@example.com", "Admin", "$2a$11$NxoWj4NA9kc7cGHzmVuRS.qs/3gY/zn7ssQ/zeq13TCKzzutaEHKi", "Quản lý", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
