using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Jobus.DataAccess.Migrations
{
    public partial class WsClientResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobus");

            migrationBuilder.CreateTable(
                name: "resource",
                schema: "jobus",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    controller = table.Column<string>(maxLength: 16, nullable: false),
                    action = table.Column<string>(maxLength: 32, nullable: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resource", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Api resources");

            migrationBuilder.CreateTable(
                name: "ws_clients",
                schema: "jobus",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    client_name = table.Column<string>(maxLength: 32, nullable: false),
                    hash = table.Column<string>(maxLength: 16, nullable: false),
                    ghost = table.Column<bool>(nullable: false, defaultValue: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ws_clients", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Table with api clients");

            migrationBuilder.CreateTable(
                name: "ws_client_resource",
                schema: "jobus",
                columns: table => new
                {
                    id_ws_client = table.Column<int>(nullable: false),
                    id_resource = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ws_client_resource", x => new { x.id_ws_client, x.id_resource });
                    table.ForeignKey(
                        name: "fk_ws_client_resource_resource_id_resource",
                        column: x => x.id_resource,
                        principalSchema: "jobus",
                        principalTable: "resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ws_client_resource_ws_clients_id_ws_client",
                        column: x => x.id_ws_client,
                        principalSchema: "jobus",
                        principalTable: "ws_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Table with controllers and actions allowed for api clients");

            migrationBuilder.CreateIndex(
                name: "controller_action_unique",
                schema: "jobus",
                table: "resource",
                columns: new[] { "controller", "action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ws_client_resource_id_resource",
                schema: "jobus",
                table: "ws_client_resource",
                column: "id_resource");

            migrationBuilder.CreateIndex(
                name: "ws_clients_hash_unique",
                schema: "jobus",
                table: "ws_clients",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ws_clients_name_unique",
                schema: "jobus",
                table: "ws_clients",
                column: "client_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ws_client_resource",
                schema: "jobus");

            migrationBuilder.DropTable(
                name: "resource",
                schema: "jobus");

            migrationBuilder.DropTable(
                name: "ws_clients",
                schema: "jobus");
        }
    }
}
