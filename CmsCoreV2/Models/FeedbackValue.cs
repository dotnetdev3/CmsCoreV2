using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class FeedbackValue : BaseEntity
    {
        [Required]
        [Display(Name = "Form Alanı Id")]
        public int? FormFieldId { get; set; }
        [StringLength(200)]
        [Display(Name = "Form Alan Adı")]
        public string FormFieldName { get; set; }
        [Display(Name = "Dosya Tipi")]
        public FieldType FieldType { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [StringLength(200)]
        [Display(Name = "Değeri")]
        public string Value { get; set; }
        [Display(Name = "Geri Bildirim Id")]
        public long FeedbackId { get; set; }
        [Display(Name = "Geri Bildirim")]
        public virtual Feedback Feedback { get; set; }
    }
}
