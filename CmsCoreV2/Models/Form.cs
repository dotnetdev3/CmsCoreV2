using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Form:BaseEntity
    {
        public Form()
        {
            IsPublished = true;
            FormFields = new HashSet<FormField>();
            LanguageId = 1;
        }
        public string FormName { get; set; }

        public string EmailTo { get; set; }

        public string EmailBcc { get; set; }

        public string EmailCc { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string Template { get; set; }

        public string ClosingDescription { get; set; }

        public string GoogleAnalyticsCode { get; set; }

        public virtual ICollection<FormField> FormFields { get; set; }

        public bool IsPublished { get; set; }

        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}
