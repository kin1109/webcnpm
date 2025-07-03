using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webcnpm1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "components",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    quantity_in_stock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__componen__3213E83F7A54634A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__3213E83FC38DBEEE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__services__3213E83FD51A5637", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F98FDBE32", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__devices__3213E83F5FC7C298", x => x.id);
                    table.ForeignKey(
                        name: "FK__devices__custome__5070F446",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "repair_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    device_id = table.Column<int>(type: "int", nullable: true),
                    problem_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "received"),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__repair_o__3213E83F4F35BD6F", x => x.id);
                    table.ForeignKey(
                        name: "FK__repair_or__assig__571DF1D5",
                        column: x => x.assigned_to,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__repair_or__custo__5535A963",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__repair_or__devic__5629CD9C",
                        column: x => x.device_id,
                        principalTable: "devices",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    repair_order_id = table.Column<int>(type: "int", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    payment_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__invoices__3213E83F3A491333", x => x.id);
                    table.ForeignKey(
                        name: "FK__invoices__repair__6754599E",
                        column: x => x.repair_order_id,
                        principalTable: "repair_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "repair_components",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    repair_order_id = table.Column<int>(type: "int", nullable: true),
                    component_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__repair_c__3213E83FE32AA1F7", x => x.id);
                    table.ForeignKey(
                        name: "FK__repair_co__compo__6383C8BA",
                        column: x => x.component_id,
                        principalTable: "components",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__repair_co__repai__628FA481",
                        column: x => x.repair_order_id,
                        principalTable: "repair_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "repair_services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    repair_order_id = table.Column<int>(type: "int", nullable: true),
                    service_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__repair_s__3213E83FEDC687D6", x => x.id);
                    table.ForeignKey(
                        name: "FK__repair_se__repai__5CD6CB2B",
                        column: x => x.repair_order_id,
                        principalTable: "repair_orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__repair_se__servi__5DCAEF64",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_devices_customer_id",
                table: "devices",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_repair_order_id",
                table: "invoices",
                column: "repair_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_components_component_id",
                table: "repair_components",
                column: "component_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_components_repair_order_id",
                table: "repair_components",
                column: "repair_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_orders_assigned_to",
                table: "repair_orders",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_repair_orders_customer_id",
                table: "repair_orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_orders_device_id",
                table: "repair_orders",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_services_repair_order_id",
                table: "repair_services",
                column: "repair_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_repair_services_service_id",
                table: "repair_services",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC5725BD89AC0",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "repair_components");

            migrationBuilder.DropTable(
                name: "repair_services");

            migrationBuilder.DropTable(
                name: "components");

            migrationBuilder.DropTable(
                name: "repair_orders");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
