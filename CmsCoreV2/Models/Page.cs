using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Page : BaseEntity
    {
        public Page()
        {
            IsPublished = true;
            ViewCount = 0;
            ChildPages = new HashSet<Page>();
            LanguageId = 1;
        }
        [StringLength(200)]
        [Required]
        [Display(Name ="Başlık")]
        public string Title { get; set; }

        [StringLength(200)]
        [Required]
        [Display(Name = "Bağlantı")]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "İçerik")]
        public string Body { get; set; }

        [Display(Name = "Görüntülenme Sayısı")]
        public long ViewCount { get; set; }


        
        public long? ParentPageId { get; set; }

        [ForeignKey("ParentPageId")]
        public Page ParentPage { get; set; }

        public virtual ICollection<Page> ChildPages { get; set; }

        [StringLength(200)]
        [Display(Name = "Seo Başlığı")]
        public string SeoTitle { get; set; }

        [Display(Name = "Seo Açıklama")]
        public string SeoDescription { get; set; }

        [Display(Name = "Seo Anahtar Kelimeler")]
        public string SeoKeywords { get; set; }

        [Display(Name = "Yayınlandı")]
        public bool IsPublished { get; set; }

        [StringLength(200)]
        [Display(Name = "Tema")]    
        public string Template  { get; set; }

        [ForeignKey("LanguageId")]
        public long LanguageId { get; set; }

        [Display(Name = "Dil")]
        public Language Language { get; set; }
       
    }
}