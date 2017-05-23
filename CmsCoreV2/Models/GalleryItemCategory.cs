using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public long? ParentCategoryId { get; set; }
        public virtual GalleryItemCategory ParentCategory { get; set; }
        public virtual ICollection<GalleryItemCategory> ChildCategories { get; set; }
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }
    }
}
