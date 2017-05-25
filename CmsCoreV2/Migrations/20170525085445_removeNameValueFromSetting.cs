using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCoreV2.Migrations
{
    public partial class removeNameValueFromSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "AppTenantId",
                table: "PostPostCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppTenantId",
                table: "GalleryItemGalleryItemCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppTenantId",
                table: "PostPostCategories");

            migrationBuilder.DropColumn(
                name: "AppTenantId",
                table: "GalleryItemGalleryItemCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Settings",
                nullable: true);
        }
    }
}
