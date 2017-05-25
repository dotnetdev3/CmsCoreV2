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
            if (context.Languages.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            var languageId = AddLanguages(context, tenant);
            AddPages(context, tenant, languageId);
            AddCustomization(context, tenant);



        }
        public static long AddLanguages(ApplicationDbContext context, AppTenant tenant)
        {
            var l = new Language();
            l.Name = "Turkish";
            l.NativeName = "Türkçe";
            l.Culture = "tr";
            l.IsActive = true;
            l.AppTenantId = tenant.AppTenantId;
            context.Languages.Add(l);
            context.SaveChanges();
            return l.Id;
        }
        public static void AddPages(ApplicationDbContext context, AppTenant tenant, long languageId)
        {
            var p = new Page();
            p.Title = "Home";
            p.Slug = "home";
            p.LanguageId = languageId;
            p.AppTenantId = tenant.AppTenantId;
            context.Pages.Add(p);
            context.SaveChanges();
        }
        public static void AddCustomization(ApplicationDbContext context, AppTenant tenant)
        {
            var customization = new Customization();
            customization.AppTenantId = tenant.AppTenantId;
            customization.ThemeId = tenant.ThemeId;
            customization.ThemeName = tenant.ThemeName;
            customization.MetaKeywords = tenant.Theme.MetaKeywords;
            customization.MetaDescription = tenant.Theme.MetaDescription;
            customization.MetaTitle = tenant.Theme.MetaTitle;
            customization.Logo = tenant.Theme.Logo;
            customization.ImageUrl = tenant.Theme.ImageUrl;
            customization.CustomCSS = tenant.Theme.CustomCSS;
            customization.CreateDate = DateTime.Now;
            customization.CreatedBy = "UserName";
            customization.UpdateDate = DateTime.Now;
            customization.UpdatedBy = "UserName";
            customization.PageTemplates = tenant.Theme.PageTemplates;
            customization.ComponentTemplates = tenant.Theme.ComponentTemplates;
            context.Customizations.Add(customization);
            context.SaveChanges();

        }
    }
}

