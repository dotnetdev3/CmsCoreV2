using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(200)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Yerel Ad")]
        public string NativeName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Kültür")]
        public string Culture { get; set; }
        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Sayfalar")]
        public virtual ICollection<Page> Pages { get; set; }
        [Display(Name = "Yazılar")]
        public virtual ICollection<Post> Posts { get; set; }
        [Display(Name = "Yazı Kategorileri")]
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        [Display(Name = "Menüler")]
        public virtual ICollection<Menu> Menus { get; set; }
        [Display(Name = "Formlar")]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
