using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Jobus.DataAccess.Migrations
{
    public partial class Job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job",
                schema: "jobus",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_ws_client = table.Column<int>(nullable: false),
                    job_type = table.Column<string>(maxLength: 8, nullable: false),
                    input = table.Column<string>(type: "json", nullable: true),
                    output = table.Column<string>(type: "json", nullable: true),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    output_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_job", x => x.id);
                    table.ForeignKey(
                        name: "fk_job_dic_job_type_job_type",
                        column: x => x.job_type,
                        principalSchema: "jobus",
                        principalTable: "dic_job_type",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_job_ws_clients_id_ws_client",
                        column: x => x.id_ws_client,
                        principalSchema: "jobus",
                        principalTable: "ws_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "job_queue",
                schema: "jobus",
                columns: table => new
                {
                    id_job = table.Column<long>(nullable: false),
                    job_type = table.Column<string>(maxLength: 8, nullable: false),
                    job_status = table.Column<string>(maxLength: 8, nullable: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    process_start_date = table.Column<DateTime>(nullable: true),
                    process_end_date = table.Column<DateTime>(nullable: true),
                    error_msg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_job_queue", x => x.id_job);
                    table.ForeignKey(
                        name: "fk_job_queue_job_id_job",
                        column: x => x.id_job,
                        principalSchema: "jobus",
                        principalTable: "job",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_job_queue_dic_job_status_job_status",
                        column: x => x.job_status,
                        principalSchema: "jobus",
                        principalTable: "dic_job_status",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_job_queue_dic_job_type_job_type",
                        column: x => x.job_type,
                        principalSchema: "jobus",
                        principalTable: "dic_job_type",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_job_job_type",
                schema: "jobus",
                table: "job",
                column: "job_type");

            migrationBuilder.CreateIndex(
                name: "ix_job_id_ws_client",
                schema: "jobus",
                table: "job",
                column: "id_ws_client");

            migrationBuilder.CreateIndex(
                name: "ix_job_queue_job_status",
                schema: "jobus",
                table: "job_queue",
                column: "job_status");

            migrationBuilder.CreateIndex(
                name: "ix_job_queue_job_type",
                schema: "jobus",
                table: "job_queue",
                column: "job_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_queue",
                schema: "jobus");

            migrationBuilder.DropTable(
                name: "job",
                schema: "jobus");
        }
    }
}
