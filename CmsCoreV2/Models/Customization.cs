using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Customization : BaseEntity
    {
        public long ThemeId { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Tema Adı")]
        public string ThemeName { get; set; }
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
        [Display(Name = "Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [StringLength(200)]
        [Display(Name = "Resim Yolu")]
        public string ImageUrl { get; set; }
        public string CustomCSS { get; set; }
        [StringLength(200)]
        [Display(Name = "Çok Lokasyonluluk")]
        public string ManyLocation { get; set; }
    }
}
