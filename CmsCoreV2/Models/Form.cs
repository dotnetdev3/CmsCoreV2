﻿using System;
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
        [Required]
        [StringLength(200)]
        [Display(Name ="Form Adı")]
        public string FormName { get; set; }
        [StringLength(200)]
        [Display(Name = "Email To")]
        public string EmailTo { get; set; }
        [StringLength(200)]
        [Display(Name = "Email BCC")]
        public string EmailBcc { get; set; }
        [StringLength(200)]
        [Display(Name = "Email CC")]
        public string EmailCc { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Şablon")]
        public string Template { get; set; }
        [Display(Name = "Kapanış Açıklaması")]
        public string ClosingDescription { get; set; }
        [Display(Name = "Google Analytics Kodu")]
        public string GoogleAnalyticsCode { get; set; }
        [Display(Name = "Form Alanları")]
        public virtual ICollection<FormField> FormFields { get; set; }
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Dil")]
        public long? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        [Display(Name = "Dil")]
        public virtual Language Language { get; set; }
    }
}
