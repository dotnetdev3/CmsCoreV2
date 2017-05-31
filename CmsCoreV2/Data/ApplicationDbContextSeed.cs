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
            AddSettings(context, tenant);           
            AddCustomization(context, tenant);
            AddMenus(context,tenant);            
            AddMenuItems(context,tenant);
            AddForms(context);
            

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
        private static void AddSettings(ApplicationDbContext context,AppTenant tenant)
        {
            var s = new Setting();
            s.AppTenantId = tenant.AppTenantId;
            s.HeaderString = "";
            s.GoogleAnalytics = "";
            s.FooterScript = "";
            s.MapLat = "";
            s.MapLon = "";
            s.SmtpUserName = "";
            s.SmtpPassword = "";
            s.SmtpHost = "";
            s.SmtpPort = "487";
            s.SmtpUseSSL = true;
            s.CreateDate = DateTime.Now;
            s.CreatedBy = "username";
            s.UpdateDate = DateTime.Now;
            s.UpdatedBy = "username";
            context.Settings.Add(s);
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
        private static void AddForms(ApplicationDbContext context)
        {
            context.AddRange(
                new Form { FormName = "Sizi Arayalım", EmailTo = "ertyeni@gmail.com", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now }
                );
            context.SaveChanges();
        }


        private static void AddMenus(ApplicationDbContext context, AppTenant tenant)
        {
            var menu = new Menu { Name = "Ana Menü", MenuLocation = "Primary", LanguageId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId=tenant.AppTenantId };
            context.AddRange(menu);
            context.SaveChanges();
        }
        private static void AddMenuItems(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new MenuItem { Name = "Hakkımızda", Url = "/hakkimizda", Position = 1, IsPublished = true, MenuId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Farkımız", Url = "/farkimiz", Position = 2, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Eğitim Modeli", Url = "/egitim-modeli", Position = 3, IsPublished = true, MenuId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kampüs", Url = "/kampus", Position = 4, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İletişim", Url = "/iletisim", Position = 5, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId });
            context.SaveChanges();
            context.AddRange(
                new MenuItem { Name = "Kurumsal", Url = "/kurumsal", Position = 1, MenuId = 1, IsPublished = true, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Vizyon Misyon", Url = "/vizyon-misyon", Position = 2, IsPublished = true, MenuId = 1, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kadromuz", Url = "/kadromuz", MenuId = 1, Position = 3, IsPublished = true, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Yönetim Kurulumuz", Url = "/yonetim-kurulumuz", MenuId = 1, IsPublished = true, Position = 4, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İngilizce Eğitimleri", Url = "/ingilizce-egitimleri", MenuId = 1, IsPublished = true, Position = 5, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bilişim Eğitimleri", Url = "/bilisim-egitimleri", MenuId = 1, IsPublished = true, Position = 6, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kişisel Gelişim", Url = "/kisisel-gelisim", MenuId = 1, IsPublished = true, Position = 7, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Sanat Eğitimleri", Url = "/sanat-egitimleri", MenuId = 1, IsPublished = true, Position = 8, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Spor Eğitimi", Url = "/spor-egitimi", MenuId = 1, IsPublished = true, Position = 9, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kurullar", Url = "/kurullar", MenuId = 1, IsPublished = true, Position = 10, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Okul Öğrenci Konseyi", Url = "/okul-ogrenci-konseyi", Position = 11, IsPublished = true, MenuId = 1, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "A+5B Eğitim Modeli", Url = "/a-5b-egitim-modeli", Position = 12, IsPublished = true, MenuId = 1, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Anaokulu", Url = "/anaokulu", MenuId = 1, Position = 13, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İlkokul", Url = "/ilkokul", MenuId = 1, Position = 14, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Ortaokul", Url = "/ortaokul", MenuId = 1, Position = 15, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Lise", Url = "/lise", MenuId = 1, Position = 16, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Derslikler", Url = "/derslikler", Position = 17, MenuId = 1, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İngilizce Laboratuvarı", Url = "/ingilizce-laboratuvari", Position = 18, IsPublished = true, MenuId = 1, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bilişim Laboratuvarı", Url = "/bilisim-laboratuvarı", MenuId = 1, Position = 19, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Fen Bilimleri Laboratuvarı", Url = "/fen-bilimleri-laboratuvari", Position = 20, IsPublished = true, MenuId = 1, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Müzik Atölyesi", Url = "/muzik-atolyesi", MenuId = 1, Position = 21, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Güzel Sanatlar Atölyesi", Url = "/guzel-sanatlar-atolyesi", MenuId = 1, IsPublished = true, Position = 22, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Spor Salonu", Url = "/spor-salonu", MenuId = 1, Position = 23, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kütüphane", Url = "/kutuphane", MenuId = 1, Position = 24, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Yemekhane", Url = "/yemekhane", MenuId = 1, Position = 25, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bahçe", Url = "/bahce", MenuId = 1, Position = 26, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bize Ulaşın", Url = "/bize-ulasin", MenuId = 1, Position = 27, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Ön Kayıt", Url = "/on-kayit", MenuId = 1, Position = 28, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Online Veli Görüşmesi", Url = "/online-veli-gorusmesi", MenuId = 1, Position = 29, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Veli-Öğrenci El Kitabı", Url = "/veli-ogrenci-el-kitabi", MenuId = 1, Position = 30, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Anket", Url = "/anket", MenuId = 1, Position = 31, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId });
            context.SaveChanges();
        }
    }
}

