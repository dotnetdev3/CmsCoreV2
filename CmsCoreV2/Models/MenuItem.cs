using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class MenuItem : BaseEntity
    {
        public MenuItem()
        {
            Position = 0;
            ChildMenuItems = new HashSet<MenuItem>();
        }
        [Required(ErrorMessage = "Menü ögesi adı zorunludur. Lütfen giriniz!")]
        [Display(Name = "Menü Öğesi Adı")]
        [StringLength (200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
        [Display(Name = "Hedef")]
        [StringLength(200)]
        public string Target { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }
        [Display(Name = "Üst Menü Öğesi")]
        public long? ParentMenuItemId { get; set; }
        [ForeignKey("ParentMenuItemId")]
        [Display(Name = "Üst Menü Öğesi")]
        public MenuItem ParentMenuItem { get; set; }
        [Display(Name = "Alt Menü Öğeleri")]
        public ICollection<MenuItem> ChildMenuItems { get; set; }
        [Required (ErrorMessage ="Menü Alanı zorunludur Lütfen giriniz!")]
        [Display(Name = "Menü")]
        public long? MenuId { get; set; }
        [ForeignKey("MenuId")]
        
        [Display(Name = "Menü")]
        public Menu Menu { get; set; }
    }
}
