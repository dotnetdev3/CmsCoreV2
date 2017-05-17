using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCoreV2.Migrations
{
    public partial class HostDbInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTenants",
                columns: table => new
                {
                    AppTenantId = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTenants", x => x.AppTenantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTenants");
        }
    }
}
