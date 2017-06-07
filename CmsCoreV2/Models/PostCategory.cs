using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            ChildCategories = new HashSet<PostCategory>();


        }
        [StringLength(200)]
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Bağlantı")]
        public string Slug { get; set; }

        [Display(Name = "Üst Kategori")]
        public long? ParentCategoryId { get; set; }
        [Display(Name = "Üst Sayfa")]
        public virtual PostCategory ParentCategory { get; set; }
        public virtual ICollection<PostCategory> ChildCategories { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Yazı Kategorileri")]
        public virtual ICollection<PostPostCategory> PostPostCategories { get; set; }
        [Display(Name = "Dil")]
        public long? LanguageId { get; set; }
        [Display(Name = "Dil")]
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}
