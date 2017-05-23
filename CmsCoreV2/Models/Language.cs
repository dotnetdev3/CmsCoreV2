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
            
        }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Culture { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Page> Pages { get; set; }

    }
}
