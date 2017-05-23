using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class PostCategory:BaseEntity
    {
        public PostCategory()
        {
            PostPostCategories = new HashSet<PostPostCategory>();
            LanguageId = 1;

        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostPostCategory> PostPostCategories { get; set; }
        public long LanguageId { get; set; }

    }
}
