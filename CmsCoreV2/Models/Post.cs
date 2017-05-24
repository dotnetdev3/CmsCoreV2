using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Post:BaseEntity
    {
        public Post()
        {
            IsPublished = true;
            ViewCount = 0;
            LanguageId = 1;
            PostPostCategories = new HashSet<PostPostCategory>();


        }
        [StringLength(200)]
        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Bağlantı")]
        public string Slug { get; set; }
        [Display(Name = "İçerik")]
        public string Body { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Resim")]
        [StringLength(200)]
        public string Photo { get; set; }
        [Display(Name = "Meta 1")]
        public string Meta1 { get; set; }
        [Display(Name = "Meta 2")]
        public string Meta2 { get; set; }
        [Display(Name = "Görüntülenme Sayısı")]
        public long ViewCount { get; set; }
        [Display(Name = "SEO Başlığı")]
        [StringLength(200)]
        public string SeoTitle { get; set; }
        [Display(Name = "SEO Açıklama")]
        public string SeoDescription { get; set; }
        [Display(Name = "SEO Anahtar Kelimeler")]
        public string SeoKeywords { get; set; }
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Yazı Kategorileri")]
        public virtual ICollection<PostPostCategory> PostPostCategories { get; set; }
        [Display(Name = "Dil")]
        public long? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        [Display(Name = "Dil")]
        public Language Language { get; set; }
    }
}
