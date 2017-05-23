using System;
using System.Collections.Generic;
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
        public string MapLat { get; set; }
        public string MapLon { get; set; }
        //MAIL
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpUseSSL { get; set; }


        public string Name { get; set; }
        public string Value { get; set; }
    }
}
