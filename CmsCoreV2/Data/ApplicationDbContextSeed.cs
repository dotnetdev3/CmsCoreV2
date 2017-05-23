using CmsCoreV2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ApplicationDbContext context, AppTenant tenant)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            // Look for any pages record.
            if (context.Pages.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            AddPages(context, tenant);
          


        }
        public static void AddPages(ApplicationDbContext context, AppTenant tenant)
        {
            var p = new Page();
            p.Title = "Home";       
            p.AppTenantId = tenant.AppTenantId;
            context.Pages.Add(p);
            context.SaveChanges();
        }
        
    }
}

