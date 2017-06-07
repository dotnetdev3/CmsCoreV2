using CmsCoreV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2
{
    public class TenantSpecificPathRewriteMiddleware<ITenant>
    {
        private readonly RequestDelegate _next;
        private readonly Func<ITenant, string> _tenantFolderResolver;
        private readonly string _pathPrefix;

        public TenantSpecificPathRewriteMiddleware(RequestDelegate next, Func<ITenant, string> tenantFolderResolver, string pathPrefix)
        {
            _next = next;
            _tenantFolderResolver = tenantFolderResolver;
            _pathPrefix = pathPrefix;

        }

        public async Task Invoke(HttpContext context)
        {
            var tenantContext = context.GetTenantContext<AppTenant>();

            if (tenantContext != null)
            {
                //remove the prefix portion of the path
                var originalPath = context.Request.Path;
                var tenantFolder = tenantContext.Tenant.Folder;
                var filePath = context.GetRouteValue("filePath");
                var newPath = new PathString($"/uploads/{tenantFolder}/{filePath}");

                context.Request.Path = newPath;

                await _next(context);

                //replace the original url after the remaining middleware has finished processing
                context.Request.Path = originalPath;
            }
        }

    }
}
