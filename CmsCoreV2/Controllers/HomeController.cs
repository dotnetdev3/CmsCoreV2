using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using CmsCoreV2.Services;
using CmsCoreV2.Models;
using CmsCoreV2.Data;
using SaasKit.Multitenancy;

namespace CmsCoreV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedbackService feedbackService;
        protected readonly AppTenant tenant;
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context, ITenant<AppTenant> tenant, IFeedbackService feedbackService)
        {
            _context = context;
            this.tenant = tenant.Value;
            this.feedbackService = feedbackService;
        }
        public IActionResult Index(string culture = "tr")
        {
            return View();
        }
        public ActionResult RedirectToDefaultLanguage()
        {
            var culture = CurrentCulture;
            if (culture == "")
            {
                culture = "tr";
            }

            return RedirectToAction("Index", new { culture = culture });
        }
        private string _currentCulture;
        private string CurrentCulture
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentCulture))
                {
                    return _currentCulture;
                }



                if (string.IsNullOrEmpty(_currentCulture))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentCulture = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentCulture;
            }
        }
        [HttpPost]
        public IActionResult PostForm(IFormCollection formCollection)
        {
            
            feedbackService.FeedbackPost(formCollection, Request.HttpContext.Connection.RemoteIpAddress.ToString(), tenant.AppTenantId);
            
            return RedirectToAction("Successful");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
