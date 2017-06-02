using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models.ViewModels
{
    public class CustomCssViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Tema Adı")]
        public string ThemeName { get; set; }
        public string CustomCSS { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdateDate { get; set; }
    }
}
