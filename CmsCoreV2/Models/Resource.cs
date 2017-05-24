using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Resource : BaseEntity
    {
        [StringLength(200)]
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Değer")]
        public string Value { get; set; }
        [Display(Name = "Dil")]
        public long? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        [Display(Name = "Dil")]
        public Language Language { get; set; }
    }
}
