using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Feedback:BaseEntity
    {
        public Feedback()
        {
            FeedbackValues = new HashSet<FeedbackValue>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Gönderme Tarihi")]
        public DateTime SentDate { get; set; }
        [Display(Name = "Form Id")]
        public int? FormId { get; set; }
        [StringLength(200)]
        [Display(Name = "Form Adı")]
        public string FormName { get; set; }
        [Display(Name = "IP")]
        public string IP { get; set; }
        [Display(Name = "Geri Bildirim Değeri")]
        public virtual ICollection<FeedbackValue> FeedbackValues { get; set; }
    }
}
