using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CmsCoreV2.Migrations.HostDb
{
    public partial class AddAgainThemes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "AppTenants",
                newName: "ThemeName");

            migrationBuilder.AddColumn<long>(
                name: "ThemeId",
                table: "AppTenants",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_AppTenants_ThemeId",
                table: "AppTenants",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTenants_Themes_ThemeId",
                table: "AppTenants",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTenants_Themes_ThemeId",
                table: "AppTenants");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_AppTenants_ThemeId",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "AppTenants");

            migrationBuilder.RenameColumn(
                name: "ThemeName",
                table: "AppTenants",
                newName: "Theme");
        }
    }
}
