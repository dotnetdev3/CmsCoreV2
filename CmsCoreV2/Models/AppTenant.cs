using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class AppTenant
    {
        public string AppTenantId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Hostname { get; set; }
        public string ThemeName { get; set; }
        public long ThemeId { get; set; }
        [ForeignKey("ThemeId")]
        public Theme Theme { get; set; }
        public string ConnectionString { get; set; }
        [StringLength(200)]
        public string Folder { get; set; }

    }

}
