using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2
{
    public static class UseExtensionMethods
    {
        public static IApplicationBuilder UsePerTenantStaticFiles<ITenant>(
    this IApplicationBuilder app,
    string pathPrefix,
    Func<ITenant, string> tenantFolderResolver)
        {
            var routeBuilder = new RouteBuilder(app);
            var routeTemplate = pathPrefix + "/{*filePath}";
            routeBuilder.MapRoute(routeTemplate, (IApplicationBuilder fork) =>
            {
                fork.UseMiddleware<TenantSpecificPathRewriteMiddleware<ITenant>>(pathPrefix, tenantFolderResolver);
                fork.UseStaticFiles();
            });
            var router = routeBuilder.Build();
            app.UseRouter(router);

            return app;
        }
    }
}
