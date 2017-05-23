using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class GalleryItemGalleryItemCategory:BaseEntity
    {
        public long GalleryItemId { get; set; }
        public virtual GalleryItem GalleryItem { get; set; }

        public long GalleryItemCategoryId { get; set; }
        public virtual GalleryItemCategory GalleryItemCategory { get; set; }
    }
}
