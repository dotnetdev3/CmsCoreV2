using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Media : BaseEntity
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Ortam Adı")]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Dosya Adı")]
        public string FileName { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Boyut")]
        public decimal Size { get; set; }

        [Required]
        [Display(Name = "Dosya Adresi")]
        public string FileUrl { get; set; }
        [Display(Name = "Dosya Tipi")]
        [StringLength(200)]
        public string FileType { get; set; }

    }
}