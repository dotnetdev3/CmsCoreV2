using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Setting:BaseEntity
    {
        //INDEX
        public string HeaderString { get; set; }
        public string GoogleAnalytics { get; set; }
        public string FooterScript { get; set; }
        [StringLength(200)]
        public string MapLat { get; set; }
        [StringLength(200)]
        public string MapLon { get; set; }
        //MAIL
        [StringLength(200)]
        public string SmtpUserName { get; set; }
        [StringLength(200)]
        public string SmtpPassword { get; set; }
        [StringLength(200)]
        public string SmtpHost { get; set; }
        [StringLength(200)]
        public string SmtpPort { get; set; }
        public bool SmtpUseSSL { get; set; }
    }
}
