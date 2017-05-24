using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace CmsCoreV2.Controllers
{
    public class HomeController : Controller
    {
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
