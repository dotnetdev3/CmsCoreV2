using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static CmsCoreV2.Models.SiteMap;
using CmsCoreV2.Models;
using CmsCoreV2.Data;
using Microsoft.EntityFrameworkCore;

namespace CmsCoreV2.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SitemapController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("sitemap")]
        public ActionResult SitemapAsync()
        {
            string baseUrl = Request.Host.Value+"/";

            // get a list of published articles
            var posts =  _context.Posts.Include("Language").Where(x=> x.IsPublished==true).ToList();

            // get a list of published articles
            var page = _context.Pages.Include("Language").Where(x => x.IsPublished == true).ToList();

            // get last modified date of the home page
            var siteMapBuilder = new SitemapBuilder();

            // add the home page to the sitemap
            siteMapBuilder.AddUrl(baseUrl, modified: DateTime.UtcNow, changeFrequency: ChangeFrequency.Daily, priority: 1.0);

            // add the blog posts to the sitemap
            foreach (var post in posts)
            {
                siteMapBuilder.AddUrl(baseUrl + post.Language.Culture + "/" + post.Slug, modified: post.UpdateDate, changeFrequency: null, priority: 0.9);
            }

            foreach (var pages in page)
            {
                siteMapBuilder.AddUrl(baseUrl + pages.Language.Culture + "/" + pages.Slug, modified: pages.UpdateDate, changeFrequency: null, priority: 0.9);
            }

            // generate the sitemap xml
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }
    }
}