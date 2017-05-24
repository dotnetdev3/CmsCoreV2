using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Redirect:BaseEntity
    {
        public Redirect()
        {
            IsActive = true;
        }
        [StringLength(200)]
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Eski Adres")]
        public string OldUrl { get; set; }
        [Required]
        [Display(Name = "Yeni Adres")]
        public string NewUrl { get; set; }
        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }
    }
}
