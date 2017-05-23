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
        // diğer dbsetler buraya eklenir

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
        // diğer dbsetler buraya eklenir

        public DbSet<CmsCoreV2.Models.Media> Media { get; set; }
    }
}
