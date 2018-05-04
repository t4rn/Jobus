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
                name: "public");

            migrationBuilder.CreateTable(
                name: "ws_clients",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    ghost = table.Column<bool>(nullable: false),
                    hash = table.Column<string>(maxLength: 16, nullable: false),
                    name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ws_clients", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Table with api clients");

            migrationBuilder.CreateIndex(
                name: "ix_ws_clients_hash",
                schema: "public",
                table: "ws_clients",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ws_clients_name",
                schema: "public",
                table: "ws_clients",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ws_clients",
                schema: "public");
        }
    }
}
