using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CmsCoreV2.Migrations
{
    public partial class AddAgainCustomizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customizations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppTenantId = table.Column<string>(nullable: true),
                    ComponentTemplates = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CustomCSS = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(maxLength: 200, nullable: true),
                    ManyLocation = table.Column<string>(maxLength: 200, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 200, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 200, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 200, nullable: true),
                    PageTemplates = table.Column<string>(maxLength: 200, nullable: true),
                    ThemeId = table.Column<long>(nullable: false),
                    ThemeName = table.Column<string>(maxLength: 200, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customizations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customizations");
        }
    }
}
