using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class Role:IdentityRole<Guid>
    {
        public int AppTenantId { get; set; }
        public virtual AppTenant AppTenant { get; set; }
    }
}
