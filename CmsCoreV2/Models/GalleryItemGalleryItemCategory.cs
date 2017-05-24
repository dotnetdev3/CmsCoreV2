using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class GalleryItemGalleryItemCategory
    {
        [Display(Name = "Galeri Öğesi")]
        public long GalleryItemId { get; set; }
        [Display(Name = "Galeri Öğesi")]
        public GalleryItem GalleryItem { get; set; }
        [Display(Name = "Galeri Öğesi Kategorisi")]
        public long GalleryItemCategoryId { get; set; }
        [Display(Name = "Galeri Öğesi Kategorisi")]
        public GalleryItemCategory GalleryItemCategory { get; set; }
    }
}
