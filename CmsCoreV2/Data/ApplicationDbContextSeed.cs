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
            AddGalleries(context, tenant);
            AddGalleryItems(context, tenant);
            AddGalleryItemCategories(context, tenant);
            AddPosts(context, tenant);
            AddPostCategories(context, tenant);
            AddGalleryItemGalleryItemCategories(context, tenant);
            AddPostPostCategories(context, tenant);




                context.SaveChangesAsync();
            }

        }
        private static void AddPostPostCategories(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new PostPostCategory { PostId=1,PostCategoryId=1 },
                new PostPostCategory { PostId = 3, PostCategoryId = 1 },
                new PostPostCategory { PostId = 4, PostCategoryId = 1 },
                new PostPostCategory { PostId = 5, PostCategoryId = 1 },
                new PostPostCategory { PostId = 6, PostCategoryId = 1 },
                new PostPostCategory { PostId = 2, PostCategoryId = 3 }


                );
        }
        private static void AddGalleryItemGalleryItemCategories(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new GalleryItemGalleryItemCategory { GalleryItemId=1,GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 4, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 5, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 6, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 8, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 12, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 14, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 15, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 16, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 17, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 18, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 19, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 20, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 21, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 22, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 23, GalleryItemCategoryId = 1 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 12, GalleryItemCategoryId = 2 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 13, GalleryItemCategoryId = 2 },
                new GalleryItemGalleryItemCategory { GalleryItemId = 14, GalleryItemCategoryId = 2 }


                );
        }
        private static void AddPostCategories(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new PostCategory { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Description=null,LanguageId=1,Name= "Haberler",ParentCategoryId=null,Slug= "haberler" },
                new PostCategory { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Description = null, LanguageId = 1, Name = "Kadromuz", ParentCategoryId = null, Slug = "kadromuz" },
                new PostCategory { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Description = "blog", LanguageId = 1, Name = "Blog", ParentCategoryId = null, Slug = "blog" }


                );
        }
        private static void AddPosts(ApplicationDbContext context, AppTenant tenant)
        {
            context.AddRange(
                new Post { CreatedBy = "username", CreateDate = DateTime.Now,  UpdatedBy = "username", UpdateDate = DateTime.Now,  Body= "<p>Donec rutrum ante augue, eu rutrum turpis finibus vel. Quisque augue sapien, ornare ut turpis ac, faucibus tempus purus. Ut consectetur aliquam ligula. Phasellus efficitur at erat sit amet tincidunt. Sed pellentesque viverra posuere. Phasellus convallis at mauris quis tincidunt. Etiam condimentum odio ut vehicula eleifend. Etiam mi metus, pulvinar a iaculis eget, hendrerit sed est. Fusce felis lectus, elementum quis ultricies ullamcorper, maximus id mi. Aliquam quis finibus ipsum. Proin ut feugiat purus. Nulla in risus eleifend, sodales quam eget, volutpat leo.</p>",Description= "küçük masa lambası",IsPublished=true,LanguageId=1,Meta1= "meta1 nedir" ,Meta2= "meta2 nedir",Photo= "https://mir-s3-cdn-cf.behance.net/project_modules/disp/46980121100769.562fb79707c8b.png",SeoDescription= "post",SeoKeywords= "post",SeoTitle= "post",Slug= "haber1",Title= "Donec rutrum ante augue, eu rutrum turpis finibus vel",ViewCount=0 },
                new Post { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas tincidunt risus tortor, vitae congue nisi maximus vitae. Nunc id sapien ut est ultrices porta. In euismod dui vitae metus venenatis vehicula. Pellentesque non lacinia odio. Fusce id lectus eu justo euismod congue. Suspendisse potenti. Pellentesque rhoncus volutpat orci sed maximus. Mauris volutpat quam ac dui rhoncus, vel vestibulum nisl tristique. Aliquam scelerisque nunc in consectetur porta. Fusce quis molestie lacus. Phasellus molestie egestas arcu, sit amet condimentum mauris facilisis ut. Ut tempor felis erat, vitae hendrerit felis fermentum at. Quisque risus justo, lacinia id nibh in, lacinia aliquet enim.</p>", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", IsPublished = true, LanguageId = 1, Meta1 = null, Meta2 = null, Photo = "http://orig11.deviantart.net/b6ab/f/2017/008/a/6/pikachu_flat_design_wallpaper_by_danecek099-daupu82.png", SeoDescription = "blog", SeoKeywords = "blog", SeoTitle = "blog", Slug = "ilk-blog-yazisi", Title = "İlk blog yazısı", ViewCount = 0 },
                new Post { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Body = "<h3>Section 1.10.32 of &quot;de Finibus Bonorum et Malorum&quot;, written by Cicero in 45 BC</h3>", Description = "culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias co", IsPublished = true, LanguageId = 1, Meta1 = null, Meta2 = null, Photo = "https://img.clipartfest.com/8c1b0745066c6b266a116c191b4cfa36_big-image-png-clipart-flat-design_2400-2400.png", SeoDescription = "yok", SeoKeywords = "yok", SeoTitle = "yok", Slug = "flat-resimler", Title = "Flat resimler", ViewCount = 0 },
                new Post { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Body = "<h3>Section 1.10.33 of &quot;de Finibus Bonorum et Malorum&quot;, written by Cicero in 45 BC</h3>", Description = "culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias co", IsPublished = true, LanguageId = 1, Meta1 = null, Meta2 = null, Photo = "https://upload.wikimedia.org/wikipedia/commons/f/f2/UTURN_logo_flat_2.png", SeoDescription = null, SeoKeywords = null, SeoTitle = null, Slug = "flat-2-resim-haber", Title = "Flat 2. resim haber ", ViewCount = 0 },
                new Post { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Body = "<h3>The standard Lorem Ipsum passage, used since the 1500s</h3>", Description = "nis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque po", IsPublished = true, LanguageId = 1, Meta1 = "a", Meta2 = "a", Photo = "https://img.clipartfest.com/7d45786fd2a7c05b9edf4f059744db6e_big-image-png-clipart-flat-design_2400-2400.png", SeoDescription = "a", SeoKeywords = "a", SeoTitle = "a", Slug = "fil-haberleri-basliyor", Title = "Fil haberleri başlıyor", ViewCount = 0 },
                new Post { CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Body = "<h3>1914 translation by H. Rackham</h3>", Description = "st, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures ha", IsPublished = true, LanguageId = 1, Meta1 = "a", Meta2 = "a", Photo = "https://openclipart.org/image/2400px/svg_to_png/193409/1399740906.png", SeoDescription = "a", SeoKeywords = "a", SeoTitle = "a", Slug = "kameralar-geldi", Title = "Kameralar geldi", ViewCount = 0 }


                );
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
            AddGalleries(context, tenant);
            context.SaveChangesAsync();
            context.Dispose();

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
                new Page { Title = "Kurumsal", Slug = "kurumsal", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Kadromuz", Slug = "kadromuz", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Hakkımızda", Slug = "hakkimizda", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Ön Kayıt", Slug = "on-kayit", Template = "PreRegistration", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Bize Ulaşın", Slug = "bize-ulasin", Template = "Contact", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId, Body= "<div class=\"row contact-method\">< div class=\"col-md-4\"><div class=\"method-item\"><i class=\"fa fa-map-marker\"></i><p class=\"sub\">ADRES</p><div class=\"detail\"><p>Bahariye Caddesi Süleymanpaşa Sokak No:2</p><p>Türkiye</p></div></div></div><div class=\"col-md-4\"><div class=\"method-item\"><i class=\"fa fa-phone\"></i><p class=\"sub\">ARA</p><div class=\"detail\"><p>Local: 216-346-26-06</p><p>Mobile: 444-3-236</p></div></div></div><div class=\"col-md-4\"><div class=\"method-item\"><i class=\"fa fa-envelope\"></i><p class=\"sub\">İLETİŞİM</p><div class=\"detail\"><p>bilisim@bilisimegitim.com</p><p>www.bilisimegitim.com</p></div></div></div></div>" },
                new Page { Title = "İş başvurusu", Slug = "is-basvurusu", Template = "JobRecourseForm", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Okul Öncesi", Slug = "okul-oncesi", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Yönetim Kurulu", Slug = "yonetim-kurulu", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "İngilizce Eğitimleri", Slug = "ingilizce-egitimleri", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId,Body= "<p>Bilgi Koleji&rsquo;nde, &ccedil;ocuklarımıza basit&ccedil;e dinleme ya da aynı metotların tekrarından oluşan, bilinen y&ouml;ntemler yerine; LSA ( Learning Style Analysis: &Ouml;ğrenme stili analizi ) ve TSA ( Teaching Style Analysis &Ouml;ğretme stili analizi ) alanlarında uzman yabancı &ouml;ğretmenlerimizle, Task Based Learning ( Durum ve Olaylara Dayalı &Ouml;ğrenme) modeliyle, her &ouml;ğrencinin farklı &ouml;ğrenme stiline uygun tasarlanmış bir m&uuml;fredatla İngilizce &ouml;ğretiyoruz. İngilizce eğitimlerimizde kullandığımız, &ldquo;Durum Bazlı &Ouml;ğrenme Modeliyle&rdquo;, doğal dil &ouml;ğrenme hızı yakalanır ve &ouml;l&ccedil;&uuml;lebilir etkin konuşma becerisi kazanılır.</p>< p > Bilgi Koleji & rsquo; de,< strong > &ldquo; A + 5B Eğitim Modeli </ strong > &rdquo; gereği olarak &ccedil; ocuklarımız İngilizce&rsquo; yi anadil d & uuml; zeyinde & ouml; ğrenirler.Amacımız & ouml; ğrencilerimizin hayatları boyunca & ouml; ğrendikleri İngilizceyi akıcı bir şekilde konuşmalarını sağlamaktır.</ p >< p > Dil Ko & ccedil; luğu uygulaması, T&uuml; rkiye & rsquo; de ilk kez Bilgi Koleji & rsquo; nde, eğitim programının i & ccedil; ine yerleştirilmiş, tamamen &ouml; ğrenci odaklı, CEFR (The Common European Framework of Reference for Languages - Avrupa Dil Kriterleri Portfolyosu) &rsquo; na paralel şekilde ilerleyen ve eğitimden maksimum verim alınmasını sağlayan &ouml; zel bir sistemdir.Bu sistemle; eğitim sırasında ve sonrasında kimin hangi &ouml; ğrenme modeline yatkın olduğu s & uuml; rekli g&ouml; zlemlenir ve kişiye & ouml; zel hazırlanan programlarla, eğitim materyallerimiz &ouml; ğrenci merkezli olarak g&uuml; ncellenir, &ouml; ğrenme s&uuml; reci denetlenir. Dil Ko&ccedil; luğu uygulaması ile & ouml; ğrencilerimizin konuşma, anlama, dinleme, ve yazma alanlarının t & uuml; m & uuml; nde & ouml; ğrenme i&ccedil; motivasyonunun y&uuml; kseltilmesi hedeflenir.</ p >< p > Bilgi Koleji & rsquo; nde ilkokulu bitiren & ouml; ğrencilerimiz; Cambridge English, Flyers sınavına tabi tutulur ve yeterlilikleri & ouml; l & ccedil; &uuml; l & uuml; r.Ortaokul & ouml; ğrencilerimizin ise Cambridge English, Preliminary English(PET) sınavlarıyla yazılı ve s&ouml; zl & uuml; olarak İngilizce seviyeleri & ouml; l & ccedil; &uuml; l & uuml; r. & Ouml; ğrencilerimiz, bu seviye tespit y&ouml; ntemleri sayesinde 8.sınıfın sonunda CEFR kriterlerine g&ouml; re B1-B2 d & uuml; zeyinde okur, anlar, dinler, konuşur ve yazar d & uuml; zeye ulaşmış olur.</ p >< p > Deneyimli & ouml; ğretmenlerimiz, ebeveynlerle işbirliği yaparak; &ouml; ğrencilerimize; edindikleri dil becerilerini okul dışında da ortaya koyabileceklerine dair g&uuml; ven duygusu kazandırırlar. & Ccedil; ocuklarımız g&ouml; rsel, işitsel ve oyuna dayalı tekniklerle, İngilizce & ouml; ğrenimini bilinen &ouml; ğrenme s&uuml; re & ccedil; leri dışında metotlarla doğal kazanım haline getirirler.</ p >< p > Uluslararası etkinlik ve projelerle & ouml; ğrendiklerini sunma ve sergileme hazzını yaşayan Bilgi Koleji &ouml; ğrencileri, dil & ouml; ğrenmenin toplumları ve kişileri yaklaştırma ve en & ouml; nemlisi tanıma yolu olduğunu da deneyimlerler.</ p >< p > &nbsp;</ p >" },
                new Page { Title = "Kişisel Gelişim Eğitimleri", Slug = "kisisel-gelisim-egitimleri", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId,Body= "<p>Bilgi Koleji&rsquo;nde &ldquo;<strong>A+5B Eğitim Modeli</strong>&rdquo; nin gereği olarak &ouml;ğrencilerimizin kişisel gelişimlerinin en az akademik gelişimleri kadar &ouml;nemli olduğunun bilincindeyiz. &Ccedil;ocuklarımızın; hayatın t&uuml;m evrelerinde &uuml;retken ve g&uuml;&ccedil;l&uuml; olabilmesi i&ccedil;in farklı kazanımlar edinmesini hedefleriz. Bu nedenle okulumuzda; kişisel gelişim alanında rehber &ouml;ğretmenlerimiz dışında var olan kurullarımızda bizimle yola &ccedil;ıkan uzmanı bilim insanlarının da desteği alınmaktadır. &Ouml;ğrencilerimiz, kişisel ve akademik gelişim alanlarında eş zamanlı ilerlemeyi hedefleyen modelimizle, gelecekte &ouml;zg&uuml;ven ve &ouml;zsaygısı tam bireyler olurlar.</p>< p > &ldquo; Kişisel Gelişim Eğitimlerimiz & rdquo; de, &ouml; ğrencilerimize hedeflerine ulaşmak i&ccedil;in motivasyonlarını y&uuml; kselterek, yaratıcı d&uuml; ş & uuml; nmeyi ve sorumluluk almayı &ouml; ğretiriz.</ p >< p > Bilgi Koleji & rsquo; de biz, okul &ouml; ncesinden başlayarak, &ccedil; ocuklarımızın eğitim hayatının tamamına yayılmış oyuna dayalı bir anlayışla, onların en y & uuml; ksek potansiyelini ortaya & ccedil; ıkararak, yaşamları boyunca etkin, verimli ve mutlu olmalarını sağlarız.</ p >< p > Okulumuzda, Kişisel Gelişim ama & ccedil; lı, her yaş grubuna & ouml; zel hazırlanmış eğlenceli etkinlik ve at&ouml; lye & ccedil; alışmalarımızı; &ouml; ğrenci ve veliyi kapsayacak bir perspektifle oluştururuz.Bilgi Koleji &ouml; ğrencileri, okul hayatları boyunca aşağıdaki temel başlıklarda kazanımlar edinirler:</ p >< ul >< li > Kendini Tanıma,</ li >< li > Gerginlikle Baş etme,</ li >< li > &Ouml; fke Kontrol&uuml; Kazanma,</ li >< li > Etik Değerleri Geliştirme,</ li >< li > İletişim Becerileri Kazanma,</ li >< li > Zamanı Verimli Kullanma,</ li >< li > Okulda ve Hayatta Zorbalıkla M&uuml; cadele </ li >< li > Stres Y & ouml; netimi,</ li >< li > Barış Eğitimi,</ li >< li > Ergenlik D & ouml; nemi Duygu Y & ouml; netimi </ li ></ ul >< p > Bu başlıklar altında yapılan eğitimleri, zaman zaman bire bir g&ouml; r & uuml; şmelerle, velilerimizi de dahil ederek &ldquo; Ebeveyn Eğitimleri&rdquo; ile de pekiştiririz.</ p >< p > Okulumuz b & uuml; nyesindeki kurullarımızda yer alan uzman pedagog ve eğitimlerimizin işbirliği ile oluşturduğumuz, &ldquo; Bilgi Akademi&rdquo; de, veli, &ouml; ğretmen ve okul & ccedil; alışanlarına y&ouml; nelik s&uuml; rekli eğitim merkezi olarak, okul y & ouml; netimiyle işbirliği halinde etkinlikler, eğitimler ve seminerler d & uuml; zenlenir.Amacımız; &ouml; ğrencilerimizin başarısı i & ccedil;in bizimle aynı yolda y&uuml; r & uuml; yen veli, &ouml; ğretmen ve okul & ccedil; alışanlarımızın da hayatın t&uuml; m alanlarında verimli ve kaliteli iletişim kurmalarını sağlamak, b&ouml; ylelikle de &ccedil; ocuklarımızın eğitim hayatına daha fazla dokunabilmektir.</ p >< p > Kurumumuz tarafından oluşturulmuş &ouml; l & ccedil; me - değerlendirme kriterleri ışığında ve & ouml; zellikle velilerle yapılan g&ouml; r & uuml; şmeler sonucunda &ouml; nceliğimiz & ouml; ğrencilerimizin kazanımlarını arttırmaktır.Bu y&ouml; ntem sayesinde aile ile &ouml; ğrenci arasındaki iletişim de g & uuml; &ccedil; lenmektedir.T & uuml; m bu s & uuml; re & ccedil; ler konularında uzman bilim insanlarının katılacağı okul i&ccedil; i ve okul dışı seminer ve etkinliklerle de desteklenmektedir.</ p >< p > &nbsp;</ p >< p > Kişisel Gelişim Eğitimleri ancak doğru okul iklimi oluşturulduğu hallerde başarıya ulaşabilir. Bu nedenle okulumuzdaki t&uuml; m idari ve akademik kadrolarımız da aynı bilin&ccedil; ve & ouml; zenle se&ccedil; ilip eğitimlere tabi tutulmaktadır.</ p >< p > &nbsp;</ p >< p > &nbsp;</ p >< p > &nbsp;</ p >" },
                new Page { Title = "Sanat Eğitimleri", Slug = "sanat-egitimleri", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId,Body= "<p>Bilgi Koleji&rsquo;nde eğitim programlarımıza y&ouml;n veren, <strong>&ldquo;A+5B Eğitim Modeli&rdquo; </strong>mizin bileşenlerinden biri de sanat eğitimidir. Bizim bakış a&ccedil;ımıza g&ouml;re, sanat eğitimi; bilim, teknoloji, kişisel gelişim ve spor eğitimleriyle birlikte bireysel ve toplumsal eğitimin vazge&ccedil;ilmezlerinden birisidir. İnsan &ccedil;ok y&ouml;nl&uuml; eğitim gereksinimi olan bir varlıktır. Bu nedenle akademik başarıyı destekleyecek bu alanlarda, &ccedil;ocuklarımızın eğilimlerini keşfediyor, uygun i&ccedil;eriklerle zenginleştirilmiş programlar &uuml;retiyor ve eğitimdeki yenilik&ccedil;i anlayışımızla daima g&uuml;ncelliyoruz.</p>< p > Ayrıca bizlere destek veren ve & ouml; ğrencilerimize sanat alanında yol g & ouml; stermek i&ccedil;in bir araya getirdiğimiz konusunda s & ouml; z sahibi olmuş, değerli kişilerden oluşan & ldquo; Sanat Kurulu&rdquo; muz da okulumuz i&ccedil;in ayrı bir gurur kaynağıdır.</ p >< p > &Ouml; ğrencilerimizin, okulumuzda alacakları sanat eğitimi, yaşam boyu s&uuml; recek bir disiplinin par&ccedil; ası olacağından, m&uuml; fredatımızın kazanımları, sadece &ouml; rg & uuml; n eğitimle değil aynı zamanda sergiler, m&uuml; zeler, kitap, dergi, yayın ve her t&uuml; rl & uuml; g & ouml; rsel - işitsel iletişim ara&ccedil; ları ile laboratuvar ortamlarıyla da desteklenmektedir. Konularında uzman kişilerden oluşan &ldquo; Sanat Kurulu&rdquo; &uuml; yelerinin g&ouml; r & uuml; ş ve &ouml; nerileri ışığında &ouml; ğrencilerimizin, sanat ve k & uuml; lt & uuml; r d&uuml; nyasına daha yakın olabilmeleri i & ccedil;in girişimlerde bulunulmaktadır. K & uuml; lt & uuml; rel alanlara geziler d&uuml; zenlenmekte ve yine kurulumuz &uuml; yelerinin desteğiyle, farklı alanlardaki sanat&ccedil; ılarla iş birlikleri yapılmaktadır. Ayrıca & ouml; ğrenciler sanatla alakalı organizasyonlara, festival ve yarışmalara katılmak i&ccedil;in teşvik edilmektedirler.</ p >< p > Kurulumuz & rsquo; da yer alan duayen sanat & ccedil; ılarımız g&ouml; zetiminde ve &ouml; ğretmenlerimizin bakış a & ccedil; ısına g&ouml; re sanat eğitiminin ama&ccedil; larından biri de bireye g & ouml; rmeyi, işitmeyi, dokunmayı, tat almayı &ouml; ğretmektir.Okulumuz i&ccedil;in &ouml; zel olarak tasarlanmış g&ouml; rsel sanatlar ve m&uuml; zik at&ouml; lyelerinde yapılan &ccedil; alışmalar, yalnızca bakmak değil, &ldquo; g & ouml; rmek & rdquo;, yalnızca duymak değil & ldquo; işitmek & rdquo;, yalnızca elle yoklamak değil, &ldquo; dokunulanı duyumsamak ve keşfetmek&rdquo; aşamalarını kapsamaktadır.</ p >< p > Bilgi Koleji & rsquo; nde sanat eğitimi, &ccedil; ocuklarımızın sadece yaratıcılıklarını ortaya &ccedil; ıkartmakla kalmaz, aynı zamanda & ouml; ğrencilerimizin kişisel gelişimlerini de destekler.Bu y&ouml; n & uuml; ile<strong> & ldquo; A + 5B Eğitim Modeli & rdquo; </ strong > mizin b & uuml; t & uuml; nl & uuml; ğ & uuml; n & uuml; n ayrılmaz bir par&ccedil; asıdır.</ p >< p > Okulumuzda, sanat derslerimiz aşağıdaki başlıklara g & ouml; re şekillenmiştir:</ p >< ul >< li >< strong > G & ouml; rsel sanatlar;</ strong > iki boyutlu sanat; resim ve &ccedil; izim gibi, ayrıca &uuml; &ccedil; boyutlu sanatlar; heykel ve seramik yapımı,</ li >< li >< strong > M & uuml; zik;</ strong > m & uuml; zik performansı, kompozisyon ve m&uuml; zik eleştirisi,</ li >< li >< strong > Drama;</ strong > drama performansı, oyun yazarlığı ve oyun eleştirmenliği,</ li >< li >< strong > Dans;</ strong > dans performansı, koreografi ve dans eleştirmenliği,</ li >< li >< strong > Medya sanatları;</ strong > fotoğraf & ccedil; ılık, sinema, video ve bilgisayar animasyonları,</ li >< li >< strong > Mimari;</ strong > bina tasarım sanatı; bir alanın g & ouml; zlemi, planlaması ve bilgisayar ortamına 3 boyutlu modellemesi,</ li ></ ul >< p > Bilgi Koleji & rsquo; nde; sanatsal duyarlılıkları eğitilen & ccedil; ocuklarımız, bu duyarlılıkla ve yaklaşımla g & ouml; zleriyle de d & uuml; ş & uuml; nerek d&uuml; nyaya bakar ve & uuml; retirler.</ p >" },
                new Page { Title = "Spor Eğitimi", Slug = "spor-egitimi", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId,Body= "<p>Bilgi Koleji&rsquo;nin eğitim i&ccedil;erikleri ve &ldquo;A+5B Eğitim Modeli&rdquo; oluşturulurken, Spor Kurulu&rsquo;muzun da katkıları alınarak; sporun &ccedil;ocuk ve gen&ccedil;lerde fiziksel, duyusal, sosyal ve bilişsel kazanımları desteklediği prensibiyle ile yola &ccedil;ıkılmıştır.</p>< p > &nbsp;</ p >< p > Okulumuzda yapılan sportif etkinlikler, &ouml; ğrencilerimizin bedensel &ouml; zellikleri, spora y&ouml; nelik ilgi alanları ve yetenekleri bilimsel testler yardımıyla saptandıktan sonra, akademik gelişim ile sağlıklı yaşam parametreleri dikkate alınarak planlanır. Bizce spor, &ccedil; ocuklarımızın sadece bedenini değil, b&uuml; t & uuml; nsel gelişimlerini son derece &ouml; nemli ve gereklidir.</ p >< p > &nbsp;</ p >< p > &Ouml; ğrencilerimizin gerek psikolojik gerekse sosyal bakımdan gelişmelerinde, oyunla birlikte sporun & ouml; nemli bir yeri vardır. &Ccedil; &uuml; nk & uuml; &ccedil; ocuklarımız bu faaliyetlere katılırken aynı zamanda grup i&ccedil; erisinde hareket etmeyi, kazanmayı veya kaybetmeyi, kurallara uymayı &ouml; ğrenmektedir.</ p >< p > En & ouml; nemlisi, &ouml; ğrencilerimiz sportif aktivitelere katılarak, kendine g & uuml; ven duygusunu kazanmakta ve toplumun bir ferdi olduğunu anlamaktadır.Bilgi Koleji&rsquo; nde & ouml; ğrencilerimiz, sportif etkinliklerimiz sayesinde bir&ccedil; ok farklı ortamda, farklı d&uuml; ş & uuml; nceden ve farklı k&uuml; lt & uuml; rden & ouml; ğrencilerle bir araya gelerek etkileşimde bulunabilmektedir.</ p >< p > &nbsp;</ p >< p > Okulumuzda uyguladığımız sportif programlar, &ouml; ğrencilerimizin yaş grubuna uygun hazırlanarak, sosyal bakımdan gelişimlerine yardımcı olmaktadır.Sportif etkinliklerimizin kazanımlarını aşağıdaki temalara g&ouml; re kurguladık;</ p >< p > &nbsp;</ p >< ul >< li > Doğayı sevme, temiz hava ve g&uuml; neşten faydalanabilme.</ li >< li > İşbirliği i & ccedil; inde & ccedil; alışma ve birlikte davranma alışkanlığı edinebilme.</ li >< li > G & ouml; rev ve sorumluluk alma, lidere uyma ve liderlik yapma.</ li >< li > Kendine g & uuml; ven duyma, yerinde ve & ccedil; abuk karar verebilme.</ li >< li > Dost & ccedil; a oynama ve yarışma, kazananı takdir etme, kaybetmeyi kabullenme, hile ve haksızlığın karşısında olabilme.</ li >< li > Demokratik hayatın gerektirdiği tavır ve alışkanlıklar kazanma.</ li ></ ul >< p > &nbsp;</ p >< p > Ayrıca Spor Kurulumuz&rsquo; da yer alan konusunda uzman spor adamları ve deneyimli & ouml; ğretmenlerimizin katkılarıyla oluşturduğumuz, eğitim i&ccedil; erikleriyle, &ccedil; ocuklarımızın bireysel anlamda hangi fiziksel aktiviteye yatkın oldukları belirlenmektedir.Bu verilere g & ouml; re hazırlanan aktiviteler eşliğinde okulumuzda, g & uuml; n & uuml; m & uuml; z & uuml; n en b & uuml; y & uuml; k sorunlarından biri olan &ldquo; &ccedil; ocuklarda obeziteyi&rdquo; &nbsp; &ouml; nlemek adına da pek &ccedil; ok faaliyet d & uuml; zenlenmektedir.</ p >< p > &nbsp; Amacımız & ccedil; ocuklarımıza t&uuml; m hayatları boyunca spor yapma alışkanlığını kazandırırken aynı zamanda velilerimizi de seminer ve etkinlikler aracılığıyla bilgilendirerek, sporu ailece kaliteli zaman ge&ccedil; irmek i&ccedil;in hayatlarına dahil etmelerini sağlamaktır. &nbsp;</ p >" },
                new Page { Title = "Bilişim Eğitimleri", Slug = "bilisim-egitimleri", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Page { Title = "Vizyon Misyon", Slug = "vizyon-misyon", Template = "Page", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId,Body= "<p>Misyon</p>< p >< strong > Var olma nedenimiz </ strong > &ccedil; ocuklarımızdır. < strong > Başarımız </ strong > ise; onların<strong> akademik</ strong > < strong > başarı </ strong > larının,</ p >< ul >< li >< strong > İngilizce </ strong > &rsquo; yle d&uuml; nyaya a&ccedil; ılması,</ li >< li >< strong > Teknoloji </ strong > yle & uuml; retime katılması,</ li >< li >< strong > Kişisel </ strong > < strong > gelişim </ strong > le mutlu bir yaşama d&ouml; n & uuml; şmesi,</ li >< li >< strong > Spor </ strong > k & uuml; lt & uuml; r & uuml; yle sağlıklı olması,</ li >< li >< strong > Sanat </ strong > la hayatına farklı bir anlam kazandırabilmesidir.</ li ></ ul >< p > &nbsp;</ p >< p > Vizyon </ p >< p > Biz BİLGİ & rsquo; yiz.İnsanlık tarihinin bu kavrama y & uuml; klediği sorumlulukla;</ p >< ul >< li >< ul style = 'list-style-type:circle' >< li > İnsanlığın bug & uuml; n & uuml; ne ve geleceğine,</ li >< li > Barışına,</ li >< li > &Uuml; retimine,</ li >< li > &Ccedil; ağdaşlığına,</ li >< li > Gelişimine,</ li >< li > İlerlemesine </ li ></ ul ></ li ></ ul >< p > katkı sağlayan,</ p >< p > &nbsp;</ p >< ul >< li >< ul style = 'list-style-type:circle' >< li > K & uuml; lt & uuml; r & uuml; ne ve milli değerlerine sahip & ccedil; ıkan,</ li >< li > &Uuml; lkesine ve D & uuml; nya & rsquo; ya değer katan,</ li >< li > &Ouml; zsaygısı, &ouml; zg & uuml; veni y&uuml; ksek bireyler yetiştirmek,</ li ></ ul ></ li ></ ul >< p > &nbsp;</ p >< p > Gelişim ve değişime &ouml; nc & uuml; l & uuml; k ederek &ldquo; Eğitim D&uuml; nyası & rdquo; nın lideri olmak.</ p >< p > &nbsp;</ p >< p > &nbsp;</ p >< p > &nbsp;</ p >< p > &nbsp;</ p >" }
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
                new Form { FormName = "İletişim", EmailTo = "mdemirci@outlook.com", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
                new Form { FormName = "Anaokulu iletişim", EmailTo = "mdemirci@outlook.com", LanguageId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }

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
            context.AddRange(
               new FormField { Name = "Ad Soyad", FormId = 3, FieldType = FieldType.fullName, Position = 1, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
               new FormField { Name = "Yaş Grubu", FormId = 3, FieldType = FieldType.smallText, Position = 2, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
               new FormField { Name = "Telefon Numarası", FormId = 3, FieldType = FieldType.telephone, Position = 3, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId },
               new FormField { Name = "Veli Adı Soyadı", FormId = 3, FieldType = FieldType.fullName, Position = 4, Required = true, Value = "", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, AppTenantId = tenant.AppTenantId }
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
            new Gallery { Name = "Galeri Sayfası", IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
            new Gallery { Name = "Anasayfa Galeri", IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });
            context.SaveChanges();


        }
        private static void AddGalleryItemCategories(ApplicationDbContext context, AppTenant tenant)
        {
            
            context.AddRange(
            new GalleryItemCategory { Name = "Flat", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now,Description= "flat", ParentCategoryId = null, Slug = "flat" },
            new GalleryItemCategory { Name = "Standart", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now, Description = "standart",ParentCategoryId=null,Slug= "standart" });
            context.SaveChanges();


        }
        private static void AddGalleryItems(ApplicationDbContext context,AppTenant tenant)
        {
           
            
            context.AddRange(
                new GalleryItem { Title = "flat1", Description = "flat 1", Position = 1, Photo = "https://colorlib.com/wp/wp-content/uploads/sites/2/2013/10/hudddle-logo.png",Video=null,Meta1= "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "Empt", Description = "Empt", Position = 11, Photo = "http://jbcdn2.b0.upaiyun.com/2017/03/752e838aa02e32c905899447c7cfeb1f.jpeg", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "bono", Description = "bono", Position = 7, Photo = "https://s-media-cache-ak0.pinimg.com/736x/ff/05/81/ff05811f23c9dda161730ac8b9688009.jpg", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "kitcube", Description = "kit", Position = 6, Photo = "http://blog.karachicorner.com/wp-content/uploads/2013/08/longshadow-flat-design-9.jpg", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "sch", Description = "sch", Position = 8, Photo = "https://s-media-cache-ak0.pinimg.com/originals/b0/6c/25/b06c2519ce91e1d0f35fa65df3cc84ce.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "m-W", Description = "m-W", Position = 11, Photo = "https://cdn.dribbble.com/users/219762/screenshots/1554839/abstract_logo_1x.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "catarse", Description = "cat", Position = 15, Photo = "https://s3.amazonaws.com/cdn.catarse/assets/logo-v-cor-negativo.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "brand", Description = "brand", Position = 11, Photo = "http://zllox.com/wp-content/uploads/2016/04/Brand-Logo-Design.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "firefox", Description = "fire", Position = 11, Photo = "https://stocklogos.com/sites/default/files/styles/blog-large/public/firefox_rebrand_flat_logo3_1.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "joystick", Description = "x", Position = 12, Photo = "http://creativeadi.com/wp-content/uploads/2015/10/super_nintendo_ios_flat_app_icon.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "fox", Description = "f", Position = 113, Photo = "https://s-media-cache-ak0.pinimg.com/736x/ae/bc/4a/aebc4a51bcf0736551b8fc34e55345a5.jpg", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "dc", Description = "DC", Position = 14, Photo = "http://cdn.designbeep.com/wp-content/uploads/2013/10/7.long-shadow-flat-logo.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "slug", Description = "sl", Position = 15, Photo = "http://designwebkit.com/wp-content/uploads/2013/07/flat-design-examples-logo-17.jpg", Video = null, Meta1 = "grid-item-height1", GalleryId = 1, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "Kalem", Description = "a", Position = 18, Photo = "http://blog.presentationload.com/wp-content/uploads/2015/09/Flat-Design-Shadow-Text.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 2, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "kareli falan bir hareket", Description = null, Position = 18, Photo = "http://1vixa0rmxmp1rwx091zccmie-wpengine.netdna-ssl.com/wp-content/uploads/2017/01/Smart-city-flat-line-web1200-x600-graphics-1.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 2, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "dikey", Description = "s", Position = 17, Photo = "http://www.talonx.com/file/2014/07/talonX-Logo-Design-Menu.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 2, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new GalleryItem { Title = "web", Description = "webtrend", Position = 17, Photo = "http://www.kordahitechnologies.com/sites/default/files/Trending%20Web%20Design11.png", Video = null, Meta1 = "grid-item-height1", GalleryId = 2, IsPublished = true, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now }


            );
            context.SaveChanges();
        }

     
        
        




    }
}

