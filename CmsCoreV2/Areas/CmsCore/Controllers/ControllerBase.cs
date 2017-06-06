using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using CmsCoreV2.Models;
using CmsCoreV2.Data;
using Z.EntityFramework.Plus;
using SaasKit.Multitenancy;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    public class ControllerBase : Controller
    {
        protected string AssetsUrl;
        protected string UploadPath;
        protected readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;

        public ControllerBase(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            this._context = context;
            if (tenant != null)
            {
                this.tenant = tenant?.Value;
                var tenantId = this.tenant.AppTenantId;

                //_context.SetFiltered<Page>().Where(x=> x.AppTenantId == tenantId);
                //_context.SetFiltered<Language>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<Media>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<Gallery>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<GalleryItem>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<GalleryItemCategory>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<PostCategory>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<PostPostCategory>().Where(x => x.AppTenantId == tenantId);
                ////_context.SetFiltered<ApplicationUser>().Where(x => x.AppTenantId == tenantId);
                ////_context.SetFiltered<Role>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<Customization>().Where(x => x.AppTenantId == tenantId);
                //_context.SetFiltered<Setting>().Where(x => x.AppTenantId == tenantId);
            }
        }
      


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appSettings = (IOptions<AppSettings>)this.HttpContext.RequestServices.GetService(typeof(IOptions<AppSettings>));
            this.AssetsUrl = appSettings.Value.AssetsUrl;
            this.UploadPath = appSettings.Value.UploadPath;
            ViewBag.AssetsUrl = this.AssetsUrl;
            ViewBag.UploadPath = this.UploadPath;
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