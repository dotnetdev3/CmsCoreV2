using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using CmsCoreV2.Services;
using Microsoft.AspNetCore.Http;
using Z.EntityFramework.Plus;

namespace CmsCoreV2.Controllers
{
    public class HomeController : Controller
    {
        protected readonly AppTenant tenant;
        private readonly ApplicationDbContext _context;
        private readonly IFeedbackService feedbackService;
        public HomeController(ApplicationDbContext context, IFeedbackService _feedbackService, AppTenant _tenant)
        {
            _context = context;
            this.feedbackService = _feedbackService;
            this.tenant = _tenant;
        }
        public string GetCategoryName(long id)
        {
            var postcat = _context.SetFiltered<PostCategory>().ToList();
            var c = _context.SetFiltered<PostPostCategory>().Where(w => w.PostId == id).FirstOrDefault();
            if (c!=null) { 
                var pcId = c.PostCategoryId;
                var val = postcat.FirstOrDefault(p => p.Id == pcId).Name;
                return val;
            }
            return "";
        }
        public IActionResult Index(string slug, string culture = "tr")
        {
            if (culture == "no")
            {
                return Redirect("/tr");
            }
            slug = slug.ToLower();
            var page = _context.SetFiltered<Page>().FirstOrDefault(p => p.Slug.ToLower() == slug);
            if (page == null || page.IsPublished == false)
            {
                var post = _context.SetFiltered<Post>().FirstOrDefault(p => p.Slug.ToLower() == slug);
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

                    post.ViewCount++;
                    postVM.ViewCount = post.ViewCount;

                    _context.Update(post);
                    _context.SaveChangesAsync();
                    return View("Post", postVM);
                }
            }
            else
            {
                if (page.IsPublished == false)
                {
                    return View("Page404");
                }
                var setting = _context.SetFiltered<Setting>().FirstOrDefault();
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

                page.ViewCount++;
                _context.Update(page);
                _context.SaveChangesAsync();
                pageVM.ViewCount = page.ViewCount;
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

        public IActionResult kindergarten()
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
        [HttpPost]
        public IActionResult Subscribe(Subscription subscription)
        { var subs = _context.Subscriptions.FirstOrDefault(s => s.Email == subscription.Email);
            if (subs==null)

            {
                subscription.AppTenantId = tenant.AppTenantId;
                subscription.CreatedBy = User.Identity.Name ?? "username";
                subscription.CreateDate = DateTime.Now;
                subscription.UpdatedBy = User.Identity.Name ?? "username";
                subscription.UpdateDate = DateTime.Now;
                subscription.SubscriptionDate = DateTime.Now;
                _context.Add(subscription);
                _context.SaveChangesAsync();
            }
           
            return RedirectToAction("Index");
           
        }

        public IActionResult CustomCss()
        {

            Customization customization = _context.Customizations.FirstOrDefault();
            if (customization != null)
            {
                return Content(customization.CustomCSS, "text/css");


            }
            return Content("", "text/css");

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
