using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using SaasKit.Multitenancy;
using Z.EntityFramework.Plus;
using Microsoft.AspNetCore.Authorization;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Authorize(Roles = "ADMIN,POST")]
    [Area("CmsCore")]
    public class PostsController : ControllerBase
    {

        public PostsController(ApplicationDbContext context, ITenant<AppTenant> tenant) : base(context, tenant)
        {

        }

        // GET: CmsCore/Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<Post>().Where(x => x.AppTenantId == tenant.AppTenantId).Include(p => p.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/Posts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: CmsCore/Posts/Create
        public IActionResult Create()
        {
            var post = new Post();
            post.CreatedBy = User.Identity.Name ?? "username";
            post.CreateDate = DateTime.Now;
            post.UpdatedBy = User.Identity.Name ?? "username";
            post.UpdateDate = DateTime.Now;
            post.AppTenantId = tenant.AppTenantId;

            

            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName");
            ViewBag.CategoryList = GetPostCategories();

            return View(post);
        }

        // POST: CmsCore/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Slug,Body,Description,Photo,Meta1,Meta2,ViewCount,SeoTitle,SeoDescription,SeoKeywords,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Post post, string categoriesHidden)
        {
            if (ModelState.IsValid)
            {
                post.CreatedBy = User.Identity.Name ?? "username";
                post.CreateDate = DateTime.Now;
                post.UpdatedBy = User.Identity.Name ?? "username";
                post.UpdateDate = DateTime.Now;
                post.AppTenantId = tenant.AppTenantId;

                _context.Add(post);
                await _context.SaveChangesAsync();
                UpdatePostPostCategories(post.Id, categoriesHidden);
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", post.LanguageId);
            return View(post);
        }
        public void UpdatePostPostCategories(long postId, string SelectedCategories)
        {
            string tenantId = tenant.AppTenantId;
            var ppc = _context.SetFiltered<PostPostCategory>().Where(x => x.AppTenantId == tenantId).Where(f => f.PostId == postId).ToList();
            var post = _context.SetFiltered<Post>().Where(x => x.AppTenantId == tenantId).Include("PostPostCategories").Where(f => f.Id == postId).FirstOrDefault();
            if (SelectedCategories != null)
            {
                foreach (var c in ppc)
                {
                    _context.PostPostCategories.Remove(c);
                }
                _context.SaveChanges();
                var cateArray = SelectedCategories.Split(',');
                
                foreach (var item in cateArray)
                {
                    post.PostPostCategories.Add(new PostPostCategory { PostId = post.Id, PostCategoryId = Convert.ToInt64(item), AppTenantId = tenantId });
                }
            }
            _context.Update(post);
            _context.SaveChanges();
        }

        // GET: CmsCore/Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include("PostPostCategories").SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", post.LanguageId);
            ViewBag.CategoryList = GetPostCategories();
            ViewBag.CheckList = post.PostPostCategories;

            post.UpdatedBy = User.Identity.Name ?? "username";
            post.UpdateDate = DateTime.Now;
            post.AppTenantId = tenant.AppTenantId;

            return View(post);
        }

        // POST: CmsCore/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,Slug,Body,Description,Photo,Meta1,Meta2,ViewCount,SeoTitle,SeoDescription,SeoKeywords,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Post post, string categoriesHidden)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.UpdatedBy = User.Identity.Name ?? "username";
                    post.UpdateDate = DateTime.Now;
                    post.AppTenantId = tenant.AppTenantId;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    UpdatePostPostCategories(post.Id, categoriesHidden);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", post.LanguageId);
            

            return View(post);
        }

        // GET: CmsCore/Posts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: CmsCore/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
        public IEnumerable<PostCategory> GetPostCategories()
        {
            var postCategories = _context.PostCategories.AsQueryable().Include("ChildCategories").ToList();
            return postCategories;

        }

   
    }
}
