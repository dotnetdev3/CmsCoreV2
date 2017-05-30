using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCoreV2.Migrations
{
    public partial class PostCategoriesController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentCategoryId",
                table: "PostCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_ParentCategoryId",
                table: "PostCategories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_PostCategories_ParentCategoryId",
                table: "PostCategories",
                column: "ParentCategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_PostCategories_ParentCategoryId",
                table: "PostCategories");

            migrationBuilder.DropIndex(
                name: "IX_PostCategories_ParentCategoryId",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "PostCategories");
        }
    }
}
