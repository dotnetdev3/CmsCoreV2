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
        [Display(Name = "Media Adı")]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Dosya Adı")]
        public string FileName { get; set; }

        [Required]
        [StringLength(800)]
        [Display(Name = "Medya Açıklaması")]
        public string Description { get; set; }

        public decimal Size { get; set; }

        [Required]
        public string FileUrl { get; set; }

        public string FileType { get; set; }

    }
}