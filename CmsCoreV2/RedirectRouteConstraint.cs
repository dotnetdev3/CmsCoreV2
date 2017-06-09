using CmsCoreV2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2
{
    public class RedirectRouteConstraint:IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if ((routeDirection == RouteDirection.IncomingRequest))
            {
                try
                {
                    var value = values[routeKey].ToString().ToLowerInvariant().Trim() + (httpContext.Request.QueryString.HasValue?httpContext.Request.QueryString.Value.ToLowerInvariant().Trim():string.Empty);
                    var context = (ApplicationDbContext)httpContext.RequestServices.GetService(typeof(ApplicationDbContext));
                    var redirect = context.Redirects.LastOrDefault(r => r.OldUrl.ToLowerInvariant() == value && r.IsActive == true);
                    if (redirect != null)
                    {
                        httpContext.Items["OldUrl"] = value;
                        httpContext.Items["NewUrl"] = redirect.NewUrl;
                        return true;
                    } else
                    {
                        return false;
                    }
                    
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

    }
}
