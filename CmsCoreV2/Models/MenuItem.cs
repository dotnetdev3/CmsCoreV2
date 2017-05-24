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
        [Required]
        [Display(Name = "Menü Öğesi")]
        [StringLength (200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
        [Display(Name = "Hedef")]
        [StringLength(200)]
        public string Target { get; set; }
        public int Position { get; set; }
        public bool IsPublished { get; set; }
        public long? ParentMenuItemId { get; set; }
        [ForeignKey("ParentMenuItemId")]
        public virtual MenuItem ParentMenuItem { get; set; }
        public ICollection<MenuItem> ChildMenuItems { get; set; }
        [Display(Name = "Menü No")]
        public long? MenuId { get; set; }
        [ForeignKey("MenuId")]
        [Display(Name = "Menü Adı")]
        public virtual Menu Menu { get; set; }
    }
}
