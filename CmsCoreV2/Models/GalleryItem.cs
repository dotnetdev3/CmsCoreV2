using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class GalleryItem:BaseEntity
    {
        public GalleryItem()
        {
            IsPublished = true;
            GalleryItemGalleryItemCategories = new HashSet<GalleryItemGalleryItemCategory>();
            Meta1 = "grid-item-height1";
        }
        [Required]
        [StringLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Resim")]
        public string Photo { get; set; }
        [Display(Name = "Video")]
        public string Video { get; set; }
        [Display(Name = "Meta 1")]
        public string Meta1 { get; set; }
        [Display(Name = "Galeri")]
        public long? GalleryId { get; set; }
        [ForeignKey("GalleryId")]
        [Display(Name = "Galeri")]
        public Gallery Gallery { get; set; }
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Galeri Öğesi Kategorileri")]
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }
    }
}
