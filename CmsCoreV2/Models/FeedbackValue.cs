using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class FeedbackValue : BaseEntity
    {
     
        [Required]
        [Display(Name = "Form Alanı")]
        public long FormFieldId { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Form Alan Adı")]
        public string FormFieldName { get; set; }
        [Display(Name = "Alan Tipi")]
        public FieldType FieldType { get; set; }
        [Display(Name = "Pozisyon")]
        public int Position { get; set; }
        [Display(Name = "Değeri")]
        public string Value { get; set; }
        [Display(Name = "Geri Bildirim")]
        public long? FeedbackId { get; set; }
        [ForeignKey("FeedbackId")]
        [Display(Name = "Geri Bildirim")]
        public Feedback Feedback { get; set; }
    }
}
