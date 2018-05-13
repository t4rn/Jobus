using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Jobus.DataAccess.Migrations
{
    public partial class Dictionaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dic_job_status",
                schema: "jobus",
                columns: table => new
                {
                    code = table.Column<string>(maxLength: 8, nullable: false),
                    description = table.Column<string>(maxLength: 32, nullable: false),
                    ghost = table.Column<bool>(nullable: false, defaultValue: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dic_job_status", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "dic_job_type",
                schema: "jobus",
                columns: table => new
                {
                    code = table.Column<string>(maxLength: 8, nullable: false),
                    description = table.Column<string>(maxLength: 32, nullable: false),
                    ghost = table.Column<bool>(nullable: false, defaultValue: false),
                    add_date = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dic_job_type", x => x.code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dic_job_status",
                schema: "jobus");

            migrationBuilder.DropTable(
                name: "dic_job_type",
                schema: "jobus");
        }
    }
}
