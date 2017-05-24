using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Language : BaseEntity
    {
        public Language()
        {
            IsActive = true;
            Pages = new HashSet<Page>();
            Posts = new HashSet<Post>();
            PostCategories = new HashSet<PostCategory>();
            Menus = new HashSet<Menu>();
            Forms = new HashSet<Form>();
        }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Culture { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
    }
}
