using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.ViewComponents
{
    public class PopularPosts : ViewComponent

    {
        private readonly ApplicationDbContext _context;


        public PopularPosts(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int total, long id)
        {
            var items = await Popular(total, id);
            return View(items);
        }
        public async Task<List<Post>> Popular(int total, long id)
        {
            return await Task.FromResult(PopulerPost(total, id).Where(w => w.IsPublished == true).ToList());
        }
        public IEnumerable<Post> PopulerPost(int total, long id)
        {
            var post = GetAll().Where(u => u.Id != id).OrderByDescending(m => m.ViewCount).Take(total).ToList();
            return post;
        }
        public IEnumerable<Models.Post> GetAll(params string[] navigations)
        {
            var set = _context.Posts.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.AsEnumerable();
        }


    }
}
