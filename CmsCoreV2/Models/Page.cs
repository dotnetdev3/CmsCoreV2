using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Page : BaseEntity
    {
        public Page()
        {
            IsPublished = true;
            ViewCount = 0;
            ChildPages = new HashSet<Page>();
            LanguageId = 1;
        }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public long ViewCount { get; set; }

        
        public long? ParentPageId { get; set; }
        [ForeignKey("ParentPageId")]
        public Page ParentPage { get; set; }
        public virtual ICollection<Page> ChildPages { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public bool IsPublished { get; set; }

        [StringLength(200)]
        public string Template  { get; set; } 

        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
       
    }
}