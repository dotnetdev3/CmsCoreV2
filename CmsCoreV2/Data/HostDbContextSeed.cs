using CmsCoreV2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Data
{
    public static class HostDbContextSeed
    {
        public static void Seed(this HostDbContext context)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            // Look for any tenants record.
            if (context.AppTenants.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            var theme = AddTheme(context);
            AddAppTenants(context, theme);

        }

        public static void AddAppTenants(HostDbContext context, Theme theme)
        {
            var appTenant = new AppTenant();
            appTenant.Name = "BilgiKoleji";
            appTenant.Title = "Bilgi Koleji";
            appTenant.Hostname = "localhost:60002";
            appTenant.ThemeName = theme.Name;
            appTenant.ConnectionString = $"Server=.;Database={appTenant.Name};Trusted_Connection=True;MultipleActiveResultSets=true";
            appTenant.Theme = theme;
            appTenant.ThemeId = theme.Id;
            context.AppTenants.Add(appTenant);

            var appTenant2 = new AppTenant();
            appTenant2.Name = "BirInsan";
            appTenant2.Title = "Bir İnsan";
            appTenant2.Hostname = "localhost:60001";
            appTenant2.ThemeName = theme.Name;
            appTenant2.ConnectionString = $"Server=.;Database={appTenant2.Name};Trusted_Connection=True;MultipleActiveResultSets=true";
            appTenant2.Theme = theme;
            appTenant2.ThemeId = theme.Id;
            context.AppTenants.Add(appTenant2);
            context.SaveChanges();
        }
        public static Theme AddTheme(HostDbContext context)
        {
            var defaultTheme = new Theme();
            defaultTheme.Name = "edugate";
            defaultTheme.Logo = "";
            defaultTheme.ImageUrl = "";
            defaultTheme.MetaDescription = "";
            defaultTheme.MetaTitle = "";
            defaultTheme.MetaKeywords = "";
            defaultTheme.PageTemplates = "";
            defaultTheme.ComponentTemplates = "";
            defaultTheme.CreateDate = DateTime.Now;
            defaultTheme.UpdateDate = DateTime.Now;
            defaultTheme.CreatedBy = "UserName";
            defaultTheme.UpdatedBy = "UserName";
            defaultTheme.CustomCSS = "";

            context.Themes.Add(defaultTheme);
            context.SaveChanges();
            return defaultTheme;
        }

    }
}
