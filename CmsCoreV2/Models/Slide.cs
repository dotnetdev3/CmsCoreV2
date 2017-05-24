using CmsCoreV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Slide : BaseEntity
    {
        public Slide()
        {
            DisplayTexts = false;
            IsPublished = true;
        }
        [StringLength(200)]
        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Alt Başlık")]
        public string SubTitle { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Resim")]
        public string Photo { get; set; }
        [Display(Name = "Video")]
        public string Video { get; set; }
        [Display(Name = "Tıklanan Buton Metni")]
        public string CallToActionText { get; set; }
        [Display(Name = "Tıklandığında Gidilecek Adres")]
        public string CallToActionUrl { get; set; }
        [Display(Name = "Metinler Gösterilsin Mi?")]
        public bool DisplayTexts { get; set; }
        [Display(Name = "Yayında Mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Slider")]
        public long? SliderId { get; set; }
        [Display(Name = "Slider")]
        [ForeignKey("SliderId")]
        public virtual Slider Slider { get; set; }

    }
}