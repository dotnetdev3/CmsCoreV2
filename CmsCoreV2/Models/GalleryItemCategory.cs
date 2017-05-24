using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class GalleryItemCategory:BaseEntity
    {
        public GalleryItemCategory()
        {
            ChildCategories = new HashSet<GalleryItemCategory>();
            GalleryItemGalleryItemCategories = new HashSet<GalleryItemGalleryItemCategory>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Bağlantı")]
        public string Slug { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Üst Kategori")]
        public long? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        [Display(Name = "Üst Kategori")]
        public virtual GalleryItemCategory ParentCategory { get; set; }
        [Display(Name = "Alt Kategoriler")]
        public virtual ICollection<GalleryItemCategory> ChildCategories { get; set; }
        [Display(Name = "Galeri Öğesi Kategorileri")]
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }
    }
}
