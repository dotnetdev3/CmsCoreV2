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
        [Required]
        [Display(Name = "Menü Adı")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Menu Konumu")]
        public string MenuLocation { get; set; }
        [Display(Name = "Menü Öğeleri")]
        public ICollection<MenuItem> MenuItems { get; set; }
        [Display(Name = "Dil")]
        public long? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        [Display(Name = "Dil")]
        public virtual Language Language { get; set; }

    }
}
