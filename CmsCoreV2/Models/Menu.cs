using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
            LanguageId = 1;
        }
        public string Name { get; set; }
        [StringLength(200)]
        public string MenuLocation { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

    }
}
