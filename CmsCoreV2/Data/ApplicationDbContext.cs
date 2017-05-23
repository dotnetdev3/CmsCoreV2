using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Models;
using Z.EntityFramework.Plus;

namespace CmsCoreV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly AppTenant tenant;
        public ApplicationDbContext() { }
        public ApplicationDbContext(AppTenant tenant)
        {
            if (tenant != null)
            {
                this.tenant = tenant;
                this.Seed(this.tenant);
                var tenantId = this.tenant.AppTenantId;

                QueryFilterManager.Filter<Page>(q => q.Where(x => x.AppTenantId == tenantId));
                QueryFilterManager.Filter<Language>(q => q.Where(x => x.AppTenantId == tenantId));
                QueryFilterManager.Filter<Media>(q => q.Where(x => x.AppTenantId == tenantId));
                QueryFilterManager.Filter<Gallery>(q => q.Where(x => x.AppTenantId == tenantId));
                QueryFilterManager.Filter<GalleryItem>(q => q.Where(x => x.AppTenantId == tenantId));
                QueryFilterManager.Filter<GalleryItemCategory>(q => q.Where(x => x.AppTenantId == tenantId));
               

                QueryFilterManager.InitilizeGlobalFilter(this);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer((tenant != null ? tenant.ConnectionString : "Server=.;Database=TenantDb;Trusted_Connection=True;MultipleActiveResultSets=true"));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackValue> FeedbackValues { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Redirect> Redirects { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<GalleryItemCategory> GalleryItemCategories { get; set; }
        public DbSet<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }

        // diğer dbsetler buraya eklenir

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<GalleryItemGalleryItemCategory>().HasKey(pc => new { pc.GalleryItemId, pc.GalleryItemCategoryId });

            builder.Entity<GalleryItemGalleryItemCategory>().HasOne(bc => bc.GalleryItem)
                .WithMany(b => b.GalleryItemGalleryItemCategories)
                .HasForeignKey(bc => bc.GalleryItemId);

            builder.Entity<GalleryItemGalleryItemCategory>().HasOne(bc => bc.GalleryItemCategory)
                .WithMany(c => c.GalleryItemGalleryItemCategories)
                .HasForeignKey(bc => bc.GalleryItemCategoryId);
        }
    }
}
