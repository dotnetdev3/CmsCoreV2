using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Subscription:BaseEntity
    {
        [Display(Name = "E-Posta")]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(200)]
        [Display(Name = "Ad-Soyad")]
        public string FullName { get; set; }
        [Display(Name = "Abone mi?")]
        public bool IsSubscribed { get; set; }
        [Display(Name = "Abone Olma Tarihi")]
        public DateTime SubscriptionDate { get; set; }
        [Display(Name = "Abonelikten Çıkma Tarihi")]
        public DateTime UnsubscriptionDate { get; set; }
    }
}
