using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class PostPostCategory
    {
        [Display(Name = "Yazı")]
        public long PostId { get; set; }
        [Display(Name = "Yazı")]
        public Post Post { get; set; }
        [Display(Name = "Yazı Kategorisi")]
        public long PostCategoryId { get; set; }
        [Display(Name = "Yazı Kategorisi")]
        public PostCategory PostCategory { get; set; }
        public string AppTenantId { get; set; }
    }
}
