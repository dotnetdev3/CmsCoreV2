using System;
using System.Collections.Generic;
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
        public string Theme { get; set; }
        public string ConnectionString { get; set; }
    }
}
