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
    [Migration("20170524090415_Mediasomefix")]
    partial class Mediasomefix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CmsCoreV2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

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

            modelBuilder.Entity("CmsCoreV2.Models.Feedback", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppTenantId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<int?>("FormId");

                    b.Property<string>("FormName")
                        .HasMaxLength(200);

                    b.Property<string>("IP");

                    b.Property<DateTime>("SentDate");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .IsRequired()
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

                    b.Property<long>("FeedbackId");

                    b.Property<int>("FieldType");

                    b.Property<int?>("FormFieldId")
                        .IsRequired();

                    b.Property<string>("FormFieldName")
                        .HasMaxLength(200);

                    b.Property<int>("Position");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value")
                        .HasMaxLength(200);

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

                    b.Property<long>("LanguageId");

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

                    b.Property<string>("Value")
                        .HasMaxLength(200);

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

                    b.Property<string>("Name");

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

                    b.Property<long>("GalleryId");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Meta1");

                    b.Property<string>("Photo");

                    b.Property<int>("Position");

                    b.Property<string>("Title");

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

                    b.Property<string>("Name");

                    b.Property<long?>("ParentCategoryId");

                    b.Property<string>("Slug");

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

                    b.Property<string>("Culture");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("NativeName");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FileType");

                    b.Property<string>("FileUrl")
                        .IsRequired();

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

                    b.Property<long>("LanguageId");

                    b.Property<string>("MenuLocation")
                        .HasMaxLength(200);

                    b.Property<string>("Name");

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

                    b.Property<long?>("MenuId");

                    b.Property<string>("Name");

                    b.Property<long?>("ParentMenuItemId");

                    b.Property<int>("Position");

                    b.Property<string>("Target");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Url");

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

                    b.Property<long>("LanguageId");

                    b.Property<long?>("ParentPageId");

                    b.Property<string>("SeoDescription");

                    b.Property<string>("SeoKeywords");

                    b.Property<string>("SeoTitle");

                    b.Property<string>("Slug");

                    b.Property<string>("Template")
                        .HasMaxLength(200);

                    b.Property<string>("Title");

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

                    b.Property<long>("LanguageId");

                    b.Property<string>("Meta1");

                    b.Property<string>("Meta2");

                    b.Property<string>("Photo");

                    b.Property<string>("SeoDescription");

                    b.Property<string>("SeoKeywords");

                    b.Property<string>("SeoTitle");

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<long>("ViewCount");

                    b.HasKey("Id");

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

                    b.Property<long>("LanguageId");

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostPostCategory", b =>
                {
                    b.Property<long>("PostId");

                    b.Property<long>("PostCategoryId");

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

                    b.Property<string>("Name");

                    b.Property<string>("NewUrl");

                    b.Property<string>("OldUrl");

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

                    b.Property<long>("LanguageId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Resources");
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

                    b.Property<string>("MapLat");

                    b.Property<string>("MapLon");

                    b.Property<string>("Name");

                    b.Property<string>("SmtpHost");

                    b.Property<string>("SmtpPassword");

                    b.Property<string>("SmtpPort");

                    b.Property<string>("SmtpUseSSL");

                    b.Property<string>("SmtpUserName");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<string>("Value");

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

                    b.Property<long>("SliderId");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

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

                    b.Property<string>("Name");

                    b.Property<string>("Template")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

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
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CmsCoreV2.Models.Form", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("GalleryId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("GalleryItemCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CmsCoreV2.Models.GalleryItem", "GalleryItem")
                        .WithMany("GalleryItemGalleryItemCategories")
                        .HasForeignKey("GalleryItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CmsCoreV2.Models.Menu", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CmsCoreV2.Models.MenuItem", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId");

                    b.HasOne("CmsCoreV2.Models.MenuItem", "ParentMenuItem")
                        .WithMany("ChildMenuItems")
                        .HasForeignKey("ParentMenuItemId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.Page", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany("Pages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CmsCoreV2.Models.Page", "ParentPage")
                        .WithMany("ChildPages")
                        .HasForeignKey("ParentPageId");
                });

            modelBuilder.Entity("CmsCoreV2.Models.PostPostCategory", b =>
                {
                    b.HasOne("CmsCoreV2.Models.PostCategory", "PostCategory")
                        .WithMany("PostPostCategories")
                        .HasForeignKey("PostCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CmsCoreV2.Models.Post", "Post")
                        .WithMany("PostPostCategories")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CmsCoreV2.Models.Resource", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CmsCoreV2.Models.Slide", b =>
                {
                    b.HasOne("CmsCoreV2.Models.Slider", "Slider")
                        .WithMany("Slides")
                        .HasForeignKey("SliderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CmsCoreV2.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
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
