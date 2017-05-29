using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CmsCoreV2.ViewComponents
{
    public class galleries:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gallery> dbSet;
        public galleries(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string name, int count = 10, string template = "Default")
        {
            var items = await GetItems(name, count);
            return View(template, items);
        }
        private Task<List<CmsCoreV2.Models.GalleryItem>> GetItems(string galleryName, int count)
        {
            List<CmsCoreV2.Models.GalleryItem> galleries = GetGalleryItems(galleryName, count).Where(w => w.IsPublished == true).ToList();
            return Task.FromResult(galleries);
        }
        public IEnumerable<GalleryItem> GetGalleryItems(string galleryName, int count)
        {
            galleryName = galleryName.ToLower();
            var gallery = Get(g => g.Name.ToLower() == galleryName && g.IsPublished == true, "GalleryItems", "GalleryItems.GalleryItemGalleryItemCategories", "GalleryItems.GalleryItemGalleryItemCategories.GalleryItemCategory");
            if (gallery != null)
            {
                var galleryItems = gallery.GalleryItems.Where(gi => gi.IsPublished == true).Take(count).ToList();
                return galleryItems;
            }
            return new List<GalleryItem>();

        }
        public Gallery Get(Expression<Func<Gallery, bool>> where, params string[] navigations)
        {
            var set = dbSet.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.Where(where).FirstOrDefault<Gallery>();
        }
       
    }
}
