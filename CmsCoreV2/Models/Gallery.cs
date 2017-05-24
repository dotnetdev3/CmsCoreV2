using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Gallery:BaseEntity
    {
        public Gallery()
        {
            IsPublished = true;
            GalleryItems = new HashSet<GalleryItem>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name="Ad")]
        public string Name { get; set; }
        [Display(Name="Yayında mı?")]
        public bool IsPublished { get; set; }
        [Display(Name="Galeri Öğeleri")]
        public virtual ICollection<GalleryItem> GalleryItems { get; set; }

    }
}
