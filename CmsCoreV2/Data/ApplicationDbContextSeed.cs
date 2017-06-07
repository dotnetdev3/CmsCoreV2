using CmsCoreV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace CmsCoreV2.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();
            AppTenant tenant = context.tenant;
            if (tenant != null) { 
            string tenantId = tenant.AppTenantId;
            // Look for any pages record.
            if (context.SetFiltered<Language>().Where(l => l.AppTenantId == tenantId).Any())
            {
                return;   // DB has been seeded
            }
            
            // Perform seed operations
            var languageId = AddLanguages(context, tenant);
            AddPages(context, tenant, languageId);
            context.SaveChanges();
            AddSettings(context, tenant);           
            AddCustomization(context, tenant);
            AddMenus(context,tenant);            
            AddMenuItems(context,tenant);
            AddPostCategories(context, tenant, languageId);
            context.SaveChanges();
            AddHomePageSlider(context, tenant);
            AddHomePageSlide(context, tenant);
            AddSecondarySlider(context, tenant);
            AddSecondarySlide(context, tenant);
            AddLogoSlider(context, tenant);
            AddLogoSlide(context, tenant);
            AddForms(context,tenant);
            AddFormFields(context, tenant);
            AddFeedbacks(context, tenant);
            AddFeedbackValues(context, tenant);
            AddGalleries(context, tenant);
            AddGalleryItems(context, tenant);




                context.SaveChangesAsync();
            }

        }
        
        private static void AddFeedbackValues(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.fullName, FormFieldId = 1, FormFieldName = "Ad Soyad", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 1 },

                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.email, FormFieldId = 2, FormFieldName = "E-posta", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 2 },
                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.telephone, FormFieldId = 3, FormFieldName = "Telefon", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 3 },
                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.radioButtons, FormFieldId = 4, FormFieldName = "Çocuğunuzu kaydettirmeyi düşündüğünüz okul aşağıdakilerden hangisidir?", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 4,Value=null },
                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.dropdownMenu, FormFieldId = 5, FormFieldName = "Çocuğunuzu kaydettirmeyi düşündüğünüz sınıf hangisidir?", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 5, Value = "Seçiniz" },
                new FeedbackValue { CreatedBy = "username", CreateDate = DateTime.Now, FeedbackId = 1, FieldType = FieldType.checkbox, FormFieldId = 6, FormFieldName = "Abonelik", UpdatedBy = "username", UpdateDate = DateTime.Now, Position = 6, Value = null }
                );
        }
        private static void AddFeedbacks(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new Feedback { CreatedBy= "username",CreateDate=DateTime.Now,FormId=1,FormName="Sizi Arayalım",UpdatedBy="username",UpdateDate=DateTime.Now,UserName="username",SentDate=DateTime.Now }

                
                );
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
           
            context.AddRange(
                new Page { Title = "Anasayfa", Slug = "anasayfa", Template = "Index", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId},
                new Page { Title = "Haberler", Slug = "haberler", Template = "Posts", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Blog", Slug = "blog", Template = "Blog", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Ön Kayıt Formu", Slug = "on-kayit-formu", Template = "PreRegistration", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "İş Başvuru Formu", Slug = "is-basvuru-formu", Template = "JobApplicationForm", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "Arama", Slug = "arama", Template = "Search", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "Anket", Slug = "anket", Template = "Survey", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "Galeri", Slug = "galeri", Template = "Gallery", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "Site Haritası", Slug = "site-haritasi", Template = "SiteMap", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now , AppTenantId = tenant.AppTenantId },
                new Page { Title = "Bize Ulaşın", Slug = "bize-ulasin", Template = "Contact", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
                );     

        }
        private static void AddPostCategories(ApplicationDbContext context,AppTenant tenant, long languageId)
        {
            context.AddRange(
                new PostCategory { Name = "Haberler", Slug = "haberler", LanguageId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new PostCategory { Name = "Kadromuz", Slug = "kadromuz", LanguageId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
                );
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
            s.SmtpUserName = "denemecvhavuzu@gmail.com";
            s.SmtpPassword = "123:Asdfg";
            s.SmtpHost = "smtp.gmail.com";
            s.SmtpPort = "587";
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
        private static void AddForms(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new Form { FormName = "Sizi Arayalım", EmailTo = "mdemirci@outlook.com", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Form { FormName = "İletişim", EmailTo = "mdemirci@outlook.com", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
                );
            context.SaveChanges();
        }

        private static void AddFormFields(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new FormField { Name = "Ad Soyad", FormId = 1, FieldType = FieldType.fullName, Position = 1, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "E-posta", FormId = 1, FieldType = FieldType.email, Position = 2, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Telefon", FormId = 1, FieldType = FieldType.telephone, Position = 3, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz okul aşağıdakilerden hangisidir?", FormId = 1, FieldType = FieldType.radioButtons, Position = 4, Required = true, Value = "Anaokulu,İlkokul,Ortaokul,Lise", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz sınıf hangisidir?", FormId = 1, FieldType = FieldType.dropdownMenu, Position = 5, Required = true, Value = "Seçiniz,1,2,3,4", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Abonelik", FormId = 1, FieldType = FieldType.checkbox, Position = 6, Required = true, Value = "Bilgi Koleji Okullarından gönderilen her türlü haber&#44; bilgi ve tanıtım içeriklerinden e-posta adresim ve telefonum aracılığıyla haberdar olmak istiyorum.", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
                );
            context.SaveChanges();
            context.AddRange(
                new FormField { Name = "Ad Soyad", FormId = 2, FieldType = FieldType.fullName, Position = 1, Required = false, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "E-posta", FormId = 2, FieldType = FieldType.email, Position = 2, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Telefon", FormId = 2, FieldType = FieldType.telephone, Position = 3, Required = false, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new FormField { Name = "Mesajınız", FormId = 2, FieldType = FieldType.largeText, Position = 3, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
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
                new MenuItem { Name = "Hakkımızda", Url = "hakkimizda", Position = 1, IsPublished = true, MenuId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Farkımız", Url = "farkimiz", Position = 2, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Eğitim Modeli", Url = "egitim-modeli", Position = 3, IsPublished = true, MenuId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kampüs", Url = "kampus", Position = 4, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İletişim", Url = "iletisim", Position = 5, MenuId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId });
            context.SaveChanges();
            context.AddRange(
                new MenuItem { Name = "Kurumsal", Url = "kurumsal", Position = 1, MenuId = 1, IsPublished = true, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Vizyon Misyon", Url = "vizyon-misyon", Position = 2, IsPublished = true, MenuId = 1, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kadromuz", Url = "kadromuz", MenuId = 1, Position = 3, IsPublished = true, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Yönetim Kurulumuz", Url = "yonetim-kurulumuz", MenuId = 1, IsPublished = true, Position = 4, ParentMenuItemId = 1, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İngilizce Eğitimleri", Url = "ingilizce-egitimleri", MenuId = 1, IsPublished = true, Position = 5, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bilişim Eğitimleri", Url = "bilisim-egitimleri", MenuId = 1, IsPublished = true, Position = 6, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kişisel Gelişim", Url = "kisisel-gelisim", MenuId = 1, IsPublished = true, Position = 7, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Sanat Eğitimleri", Url = "sanat-egitimleri", MenuId = 1, IsPublished = true, Position = 8, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Spor Eğitimi", Url = "spor-egitimi", MenuId = 1, IsPublished = true, Position = 9, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kurullar", Url = "kurullar", MenuId = 1, IsPublished = true, Position = 10, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Okul Öğrenci Konseyi", Url = "okul-ogrenci-konseyi", Position = 11, IsPublished = true, MenuId = 1, ParentMenuItemId = 2, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "A+5B Eğitim Modeli", Url = "a-5b-egitim-modeli", Position = 12, IsPublished = true, MenuId = 1, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Anaokulu", Url = "anaokulu", MenuId = 1, Position = 13, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İlkokul", Url = "ilkokul", MenuId = 1, Position = 14, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Ortaokul", Url = "ortaokul", MenuId = 1, Position = 15, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Lise", Url = "lise", MenuId = 1, Position = 16, IsPublished = true, ParentMenuItemId = 3, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Derslikler", Url = "derslikler", Position = 17, MenuId = 1, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "İngilizce Laboratuvarı", Url = "ingilizce-laboratuvari", Position = 18, IsPublished = true, MenuId = 1, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bilişim Laboratuvarı", Url = "bilisim-laboratuvarı", MenuId = 1, Position = 19, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Fen Bilimleri Laboratuvarı", Url = "fen-bilimleri-laboratuvari", Position = 20, IsPublished = true, MenuId = 1, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Müzik Atölyesi", Url = "muzik-atolyesi", MenuId = 1, Position = 21, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Güzel Sanatlar Atölyesi", Url = "guzel-sanatlar-atolyesi", MenuId = 1, IsPublished = true, Position = 22, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Spor Salonu", Url = "spor-salonu", MenuId = 1, Position = 23, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Kütüphane", Url = "kutuphane", MenuId = 1, Position = 24, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Yemekhane", Url = "yemekhane", MenuId = 1, Position = 25, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bahçe", Url = "bahce", MenuId = 1, Position = 26, IsPublished = true, ParentMenuItemId = 4, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Bize Ulaşın", Url = "bize-ulasin", MenuId = 1, Position = 27, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Ön Kayıt", Url = "on-kayit-formu", MenuId = 1, Position = 28, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Online Veli Görüşmesi", Url = "online-veli-gorusmesi", MenuId = 1, Position = 29, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Veli-Öğrenci El Kitabı", Url = "veli-ogrenci-el-kitabi", MenuId = 1, Position = 30, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new MenuItem { Name = "Anket", Url = "anket", MenuId = 1, Position = 31, IsPublished = true, ParentMenuItemId = 5, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId });
            context.SaveChanges();
        }

        private static void AddHomePageSlider(ApplicationDbContext context, AppTenant tenant)
        {
            var slider = new Slider();
            slider.AppTenantId = tenant.AppTenantId;

            slider.IsPublished = true;
            slider.Name = "Anasayfa Slider";
            slider.Template = "Default";
            slider.CreateDate = DateTime.Now;
            slider.CreatedBy = "username";
            slider.UpdateDate = DateTime.Now;
            slider.UpdatedBy = "username";
            slider.Slides = new HashSet<Slide>();

            context.Sliders.Add(slider);
            context.SaveChanges();


        }

        private static void AddHomePageSlide(ApplicationDbContext context, AppTenant tenant)
        {
            var s1 = new Slide();
            s1.AppTenantId = tenant.AppTenantId;

            s1.Title = "Başlık1";
            s1.SubTitle = "Alt Başlık1";
            s1.Description = "Açıklama1";
            s1.Position = 1;
            s1.Video = "/uploads/3383465.mov";
            s1.CallToActionText = "Buton1";
            s1.CallToActionUrl = "#";
            s1.DisplayTexts = false;
            s1.IsPublished = true;
            s1.SliderId = 1;
            s1.CreateDate = DateTime.Now;
            s1.CreatedBy = "username";
            s1.UpdateDate = DateTime.Now;
            s1.UpdatedBy = "username";
            context.Slides.Add(s1);

            var s2 = new Slide();
            s2.AppTenantId = tenant.AppTenantId;

            s2.Title = "Başlık2";
            s2.SubTitle = "Alt Başlık2";
            s2.Description = "Açıklama2";
            s2.Position = 1;
            s2.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s2.CallToActionText = "Buton2";
            s2.CallToActionUrl = "#";
            s2.DisplayTexts = false;
            s2.IsPublished = true;
            s2.SliderId = 1;
            s2.CreateDate = DateTime.Now;
            s2.CreatedBy = "username";
            s2.UpdateDate = DateTime.Now;
            s2.UpdatedBy = "username";
            context.Slides.Add(s2);

            var s3 = new Slide();
            s3.AppTenantId = tenant.AppTenantId;

            s3.Title = "Başlık3";
            s3.SubTitle = "Alt Başlık3";
            s3.Description = "Açıklama3";
            s3.Position = 1;
            s3.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s3.CallToActionText = "Buton3";
            s3.CallToActionUrl = "#";
            s3.DisplayTexts = false;
            s3.IsPublished = true;
            s3.SliderId = 1;
            s3.CreateDate = DateTime.Now;
            s3.CreatedBy = "username";
            s3.UpdateDate = DateTime.Now;
            s3.UpdatedBy = "username";
            context.Slides.Add(s3);
            context.SaveChanges();


        }

        private static void AddSecondarySlider(ApplicationDbContext context, AppTenant tenant)
        {
            var slider = new Slider();
            slider.AppTenantId = tenant.AppTenantId;

            slider.IsPublished = true;
            slider.Name = "Anasayfa İkinci Slider";
            slider.Template = "Secondary";
            slider.CreateDate = DateTime.Now;
            slider.CreatedBy = "username";
            slider.UpdateDate = DateTime.Now;
            slider.UpdatedBy = "username";
            slider.Slides = new HashSet<Slide>();

            context.Sliders.Add(slider);
            context.SaveChanges();


        }

        private static void AddSecondarySlide(ApplicationDbContext context, AppTenant tenant)
        {
            var s1 = new Slide();
            s1.AppTenantId = tenant.AppTenantId;

            s1.Title = "Başlık4";
            s1.SubTitle = "Alt Başlık4";
            s1.Description = "Açıklama4";
            s1.Position = 2;
            s1.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s1.CallToActionText = "Buton4";
            s1.CallToActionUrl = "#";
            s1.DisplayTexts = false;
            s1.IsPublished = true;
            s1.SliderId = 2;
            s1.CreateDate = DateTime.Now;
            s1.CreatedBy = "username";
            s1.UpdateDate = DateTime.Now;
            s1.UpdatedBy = "username";
            context.Slides.Add(s1);

            var s2 = new Slide();
            s2.AppTenantId = tenant.AppTenantId;

            s2.Title = "Başlık5";
            s2.SubTitle = "Alt Başlık5";
            s2.Description = "Açıklama5";
            s2.Position = 2;
            s2.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s2.CallToActionText = "Buton5";
            s2.CallToActionUrl = "#";
            s2.DisplayTexts = false;
            s2.IsPublished = true;
            s2.SliderId = 2;
            s2.CreateDate = DateTime.Now;
            s2.CreatedBy = "username";
            s2.UpdateDate = DateTime.Now;
            s2.UpdatedBy = "username";
            context.Slides.Add(s2);
            context.SaveChanges();


        }

        private static void AddLogoSlider(ApplicationDbContext context, AppTenant tenant)
        {
            var slider = new Slider();
            slider.AppTenantId = tenant.AppTenantId;

            slider.IsPublished = true;
            slider.Name = "Logo Slider";
            slider.Template = "LogoSlider";
            slider.CreateDate = DateTime.Now;
            slider.CreatedBy = "username";
            slider.UpdateDate = DateTime.Now;
            slider.UpdatedBy = "username";
            slider.Slides = new HashSet<Slide>();

            context.Sliders.Add(slider);
            context.SaveChanges();


        }

        private static void AddLogoSlide(ApplicationDbContext context, AppTenant tenant)
        {

            var s1 = new Slide();
            s1.AppTenantId = tenant.AppTenantId;

            s1.Title = "Başlık6";
            s1.SubTitle = "Alt Başlık6";
            s1.Description = "Açıklama6";
            s1.Position = 3;
            s1.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s1.CallToActionText = "Buton6";
            s1.CallToActionUrl = "#";
            s1.DisplayTexts = false;
            s1.IsPublished = true;
            s1.SliderId = 3;
            s1.CreateDate = DateTime.Now;
            s1.CreatedBy = "username";
            s1.UpdateDate = DateTime.Now;
            s1.UpdatedBy = "username";
            context.Slides.Add(s1);

            var s2 = new Slide();
            s2.AppTenantId = tenant.AppTenantId;

            s2.Title = "Başlık7";
            s2.SubTitle = "Alt Başlık7";
            s2.Description = "Açıklama7";
            s2.Position = 3;
            s2.Photo = "/uploads/5-2017/9a2ef92e2e0ca0fb061171e27596dfeb.png";
            s2.CallToActionText = "Buton7";
            s2.CallToActionUrl = "#";
            s2.DisplayTexts = false;
            s2.IsPublished = true;
            s2.SliderId = 3;
            s2.CreateDate = DateTime.Now;
            s2.CreatedBy = "username";
            s2.UpdateDate = DateTime.Now;
            s2.UpdatedBy = "username";
            context.Slides.Add(s2);
            context.SaveChanges();


        }



        private static void AddGalleries(ApplicationDbContext context,AppTenant tenant)
        {




            context.AddRange(
            new Gallery { Name = "Galeri Sayfası", IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, },
            new Gallery { Name = "Anasayfa Galeri", IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, });
            context.SaveChanges();


        }
        private static void AddGalleryItems(ApplicationDbContext context,AppTenant tenant)
        {
           
            var galleryItem = new GalleryItem { Title = "Ana Galeri Item1", Description = "Ana Galeri Item1", Position = 1, Photo = "/uploads/946468297image-slider-2.jpg", GalleryId=1,IsPublished=true,CreatedBy= "username", CreateDate= DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now }; galleryItem.AppTenantId = tenant.AppTenantId;
            context.AddRange(galleryItem);
            context.SaveChanges();
        }

     
        
        




    }
}

