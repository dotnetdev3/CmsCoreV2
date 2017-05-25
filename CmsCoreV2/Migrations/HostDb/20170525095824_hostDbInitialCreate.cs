using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CmsCoreV2.Migrations.HostDb
{
    public partial class hostDbInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComponentTemplates = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: false),
                    CustomCSS = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Logo = table.Column<string>(maxLength: 200, nullable: true),
                    ManyLocation = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 200, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 200, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PageTemplates = table.Column<string>(maxLength: 200, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTenants",
                columns: table => new
                {
                    AppTenantId = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ThemeId = table.Column<long>(nullable: false),
                    ThemeName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTenants", x => x.AppTenantId);
                    table.ForeignKey(
                        name: "FK_AppTenants_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTenants_ThemeId",
                table: "AppTenants",
                column: "ThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTenants");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
