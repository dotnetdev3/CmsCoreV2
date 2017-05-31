using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CmsCoreV2.Data;
using CmsCoreV2.Models;

namespace CmsCoreV2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170530084941_PostCategoriesController")]
    partial class PostCategoriesController
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CmsCoreV2.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AppTenantId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Customization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("ComponentTemplates");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("CustomCSS");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Logo")
                        .HasMaxLength(200);

                    b.Property<string>("ManyLocation");

                    b.Property<string>("MetaDescription");

                    b.Property<string>("MetaKeywords");

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(200);

                    b.Property<string>("PageTemplates");

                    b.Property<long>("ThemeId");

                    b.Property<string>("ThemeName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Customizations");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Feedback", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<long>("FormId");

                    b.Property<string>("FormName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("IP")
                        .IsRequired();

                    b.Property<DateTime>("SentDate");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CmsCoreV2.Models.FeedbackValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<long?>("FeedbackId");

                    b.Property<int>("FieldType");

                    b.Property<long>("FormFieldId");

                    b.Property<string>("FormFieldName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Position");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.ToTable("FeedbackValues");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Form", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("ClosingDescription");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<string>("EmailBcc")
                        .HasMaxLength(200);

                    b.Property<string>("EmailCc")
                        .HasMaxLength(200);

                    b.Property<string>("EmailTo")
                        .HasMaxLength(200);

                    b.Property<string>("FormName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("GoogleAnalyticsCode");

                    b.Property<bool>("IsPublished");

                    b.Property<long?>("LanguageId");

                    b.Property<string>("Template");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("CmsCoreV2.Models.FormField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<int>("FieldType");

                    b.Property<long?>("FormId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Position");

                    b.Property<bool>("Required");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormFields");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Gallery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<long?>("GalleryId");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Meta1");

                    b.Property<string>("Photo");

                    b.Property<int>("Position");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Video");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId");

                    b.ToTable("GalleryItems");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItemCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("ParentCategoryId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("GalleryItemCategories");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItemGalleryItemCategory", b =>
                {
                    b.Property<long>("GalleryItemId");

                    b.Property<long>("GalleryItemCategoryId");

                    b.Property<string>("AppTenantId");

                    b.HasKey("GalleryItemId", "GalleryItemCategoryId");

                    b.HasIndex("GalleryItemCategoryId");

                    b.ToTable("GalleryItemGalleryItemCategories");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Language", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Media", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<string>("FileName")
                        .HasMaxLength(200);

                    b.Property<string>("FileType")
                        .HasMaxLength(200);

                    b.Property<string>("FileUrl");

                    b.Property<decimal>("Size");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<long?>("LanguageId");

                    b.Property<string>("MenuLocation")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CmsCoreV2.Models.MenuItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished");

                    b.Property<long?>("MenuId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("ParentMenuItemId");

                    b.Property<int>("Position");

                    b.Property<string>("Target")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("ParentMenuItemId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Page", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished");

                    b.Property<long?>("LanguageId");

                    b.Property<long?>("ParentPageId");

                    b.Property<string>("SeoDescription");

                    b.Property<string>("SeoKeywords");

                    b.Property<string>("SeoTitle")
                        .HasMaxLength(200);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Template")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<long>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ParentPageId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublished");

                    b.Property<long?>("LanguageId");

                    b.Property<string>("Meta1");

                    b.Property<string>("Meta2");

                    b.Property<string>("Photo")
                        .HasMaxLength(200);

                    b.Property<string>("SeoDescription");

                    b.Property<string>("SeoKeywords");

                    b.Property<string>("SeoTitle")
                        .HasMaxLength(200);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<long>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<long?>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("ParentCategoryId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostPostCategory", b =>
                {
                    b.Property<long>("PostId");

                    b.Property<long>("PostCategoryId");

                    b.Property<string>("AppTenantId");

                    b.HasKey("PostId", "PostCategoryId");

                    b.HasIndex("PostCategoryId");

                    b.ToTable("PostPostCategories");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Redirect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("NewUrl")
                        .IsRequired();

                    b.Property<string>("OldUrl")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Redirects");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<long?>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("FooterScript");

                    b.Property<string>("GoogleAnalytics");

                    b.Property<string>("HeaderString");

                    b.Property<string>("MapLat")
                        .HasMaxLength(200);

                    b.Property<string>("MapLon")
                        .HasMaxLength(200);

                    b.Property<string>("SmtpHost")
                        .HasMaxLength(200);

                    b.Property<string>("SmtpPassword")
                        .HasMaxLength(200);

                    b.Property<string>("SmtpPort")
                        .HasMaxLength(200);

                    b.Property<bool>("SmtpUseSSL");

                    b.Property<string>("SmtpUserName")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Slide", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<string>("CallToActionText");

                    b.Property<string>("CallToActionUrl");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Description");

                    b.Property<bool>("DisplayTexts");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Photo");

                    b.Property<int>("Position");

                    b.Property<long?>("SliderId");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Video");

                    b.HasKey("Id");

                    b.HasIndex("SliderId");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Slider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Template")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CmsCoreV2.Models.FeedbackValue", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Feedback", "Feedback")
                        .WithMany("FeedbackValues")
                        .HasForeignKey("FeedbackId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Form", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("Forms")
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.FormField", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Form", "Form")
                        .WithMany("FormFields")
                        .HasForeignKey("FormId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItem", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Gallery", "Gallery")
                        .WithMany("GalleryItems")
                        .HasForeignKey("GalleryId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItemCategory", b =>
                {
                    b.HasOne("CmsCoreV2.Models.GalleryItemCategory", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.GalleryItemGalleryItemCategory", b =>
                {
                    b.HasOne("CmsCoreV2.Models.GalleryItemCategory", "GalleryItemCategory")
                        .WithMany("GalleryItemGalleryItemCategories")
                        .HasForeignKey("GalleryItemCategoryId");

                    b.HasOne("CmsCoreV2.Models.GalleryItem", "GalleryItem")
                        .WithMany("GalleryItemGalleryItemCategories")
                        .HasForeignKey("GalleryItemId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Menu", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("Menus")
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.MenuItem", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CmsCoreV2.Models.MenuItem", "ParentMenuItem")
                        .WithMany("ChildMenuItems")
                        .HasForeignKey("ParentMenuItemId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Page", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("Pages")
                        .HasForeignKey("LanguageId");

                    b.HasOne("CmsCoreV2.Models.Page", "ParentPage")
                        .WithMany("ChildPages")
                        .HasForeignKey("ParentPageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Post", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("Posts")
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostCategory", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("PostCategories")
                        .HasForeignKey("LanguageId");

                    b.HasOne("CmsCoreV2.Models.PostCategory", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostPostCategory", b =>
                {
                    b.HasOne("CmsCoreV2.Models.PostCategory", "PostCategory")
                        .WithMany("PostPostCategories")
                        .HasForeignKey("PostCategoryId");

                    b.HasOne("CmsCoreV2.Models.Post", "Post")
                        .WithMany("PostPostCategories")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Resource", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Slide", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Slider", "Slider")
                        .WithMany("Slides")
                        .HasForeignKey("SliderId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CmsCoreV2.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
