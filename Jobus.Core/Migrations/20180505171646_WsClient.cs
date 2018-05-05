using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Jobus.Core.Migrations
{
    public partial class WsClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobus");

            migrationBuilder.CreateTable(
                name: "ws_clients",
                schema: "jobus",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    client_name = table.Column<string>(maxLength: 32, nullable: false),
                    hash = table.Column<string>(maxLength: 16, nullable: false),
                    ghost = table.Column<bool>(nullable: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ws_clients", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Table with api clients");

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
                name: "ws_clients",
                schema: "jobus");
        }
    }
}
