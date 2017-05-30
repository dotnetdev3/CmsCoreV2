using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using CmsCoreV2.Data;
using CmsCoreV2.Models;

namespace CmsCoreV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public string GetCategoryName(long id)
        {
            var postcat = _context.PostCategories.ToList();
            var pcId = _context.PostPostCategories.Where(w => w.PostId == id).FirstOrDefault().PostCategoryId;
            var val = postcat.FirstOrDefault(p => p.Id == pcId).Name;
            return val;
        }
        public IActionResult Index(string slug, string culture = "tr")
        {
            slug = slug.ToLower();
            var page = _context.Pages.FirstOrDefault(p => p.Slug.ToLower() == slug);
            if (page == null || page.IsPublished == false)
            {
                var post = _context.Posts.FirstOrDefault(p => p.Slug.ToLower() == slug);
                if (post == null)
                {
                    return View("Page404");
                }
                else
                {
                    if (post == null || post.IsPublished == false)
                    {
                        return View("Page404");
                    }
                    PostViewModel postVM = new PostViewModel();
                    postVM.Id = post.Id;
                    postVM.Title = post.Title;
                    postVM.Slug = post.Slug;
                    postVM.Body = post.Body;
                    postVM.CategoryName = GetCategoryName(post.Id);
                    postVM.Description = post.Description;
                    postVM.IsPublished = post.IsPublished;
                    postVM.CreateDate = post.CreateDate;
                    postVM.SeoTitle = post.SeoTitle;
                    postVM.SeoDescription = post.SeoDescription;
                    postVM.SeoKeywords = post.SeoKeywords;
                    postVM.Photo = post.Photo;
                    postVM.ViewCount = post.ViewCount;
                    return View("Post", postVM);
                }
            }
            else
            {
                if (page.IsPublished == false)
                {
                    return View("Page404");
                }
                var setting = _context.Settings.FirstOrDefault();
                ViewBag.MapLat = setting.MapLat;
                ViewBag.MapLon = setting.MapLon;
                PageViewModel pageVM = new PageViewModel();
                pageVM.Id = page.Id;
                pageVM.Title = page.Title;
                pageVM.Slug = page.Slug;
                pageVM.Body = page.Body;
                pageVM.Template = page.Template;
                pageVM.SeoTitle = page.SeoTitle;
                pageVM.SeoKeywords = page.SeoKeywords;
                pageVM.SeoDescription = page.SeoDescription;
                if (!String.IsNullOrEmpty(page.Template))
                {
                    return View(page.Template, pageVM);
                }
                return View(pageVM);
            }
        }

        
        public IActionResult Page404()
        {
            return View();
        }
        public IActionResult Successful()
        {
            return View("Successful");
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
