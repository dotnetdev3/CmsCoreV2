using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    public class ControllerBase : Controller
    {
        protected string AssetsUrl;
        protected string UploadPath;


        public ControllerBase()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {             
            ViewBag.UploadPath = "C:\\Users\\Admin\\Source\\Repos\\CmsCoreV2\\CmsCoreV2\\-2017\\";
            base.OnActionExecuting(filterContext);

        }



        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}