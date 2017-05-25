using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Theme
    {
        public long Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [StringLength(200)]
        [Display(Name = "Resim Yolu")]
        public string ImageUrl { get; set; }
        [StringLength(200)]
        [Display(Name = "Sayfa Şablonu")]
        public string PageTemplates { get; set; }
        [StringLength(200)]
        [Display(Name = "Component Şablonu")]
        public string ComponentTemplates { get; set; }
        [StringLength(200)]
        public string Logo { get; set; }
        [StringLength(200)]
        [Display(Name = "Meta Başlık")]
        public string MetaTitle { get; set; }
        [StringLength(200)]
        [Display(Name = "Meta Açıklama")]
        public string MetaDescription { get; set; }
        [StringLength(200)]
        [Display(Name = "Anahtar kelimeler")]
        public string MetaKeywords { get; set; }
        public string CustomCSS { get; set; }
        [Display(Name = "Çok lokasyonluluk")]
        public string ManyLocation { get; set; } 
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Oluşturan Kişi")]
        public string CreatedBy { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Güncelleyen Kişi")]
        public string UpdatedBy { get; set; }
    }
}
