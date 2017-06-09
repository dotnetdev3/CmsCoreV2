using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.ViewComponents
{
    public class ListPosts : ViewComponent
    {
        private readonly ApplicationDbContext _context;
            
        public ListPosts(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryNames = "", int count = 8)
        {
            var items = await GetItems(categoryNames, count);
            return View(items);
        }
        private async Task<List<Post>> GetItems(string categoryNames, int count)
        {
            List<Post> posts = GetPostsInCategoryNames(categoryNames.Split(','), count).Where(w => w.IsPublished == true).ToList();
            return await Task.FromResult(posts);
        }

        public IEnumerable<Post> GetPostsByCategoryNames(string categoryNames, int count, long id)
        {
            string[] categories;
            if (categoryNames == "")
            {
                categories = new string[0];
            }
            else
            {
                categories = categoryNames.Split(',');
            }

            for (var i = 0; i < categories.Length; i++)
            {
                categories[i] = categories[i].Trim().ToLower();
            }

            var posts = GetPostsInCategoryNames(categories, count, id);
            return posts;
        }

        public IEnumerable<Post> GetPostsInCategoryNames(string[] categories, int count )
        {
            if (categories.Length > 0)
            {
                return (from pc in _context.PostPostCategories join p in _context.Posts on pc.PostId equals p.Id join c in _context.PostCategories on pc.PostCategoryId equals c.Id where (categories.Length > 0 ? categories.Contains(c.Name.ToLower()) : true) orderby p.CreateDate descending select p).Take(count).ToList();
            }
            else
            {
                return (from p in _context.Posts orderby p.CreateDate descending select p).Take(count).ToList();
            }
        }
        public IEnumerable<Post> GetPostsInCategoryNames(string[] categories, int count, long? id)
        {
            if (categories.Length > 0)
            {
                return (from pc in _context.PostPostCategories join p in _context.Posts on pc.PostId equals p.Id join c in _context.PostCategories on pc.PostCategoryId equals c.Id where (categories.Length > 0 ? categories.Contains(c.Name.ToLower()) : true) orderby p.CreateDate descending select p).Where(c => c.Id != id).Take(count).ToList();
            }
            else
            {
                return (from p in _context.Posts orderby p.CreateDate descending select p).Where(c => c.Id != id).Take(count).ToList();
            }

        }
    }
}
