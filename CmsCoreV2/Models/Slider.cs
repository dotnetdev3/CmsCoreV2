using CmsCoreV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Slider : BaseEntity
    {
        public Slider()
        {
            IsPublished = true;
            Slides = new HashSet<Slide>();
        }
        [StringLength(200)]
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Slidelar")]
        public virtual ICollection<Slide> Slides { get; set; }
        [StringLength(200)]
        [Display(Name = "Şablon")]
        public string Template { get; set; }
    }
}