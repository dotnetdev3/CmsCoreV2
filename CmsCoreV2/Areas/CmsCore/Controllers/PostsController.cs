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

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;


        public PostsController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;

        }

        // GET: CmsCore/Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Language);
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture");
            return View(post);
        }

        // POST: CmsCore/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Slug,Body,Description,Photo,Meta1,Meta2,ViewCount,SeoTitle,SeoDescription,SeoKeywords,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Post post)
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
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", post.LanguageId);
            return View(post);
        }

        // GET: CmsCore/Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", post.LanguageId);
            return View(post);
        }

        // POST: CmsCore/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,Slug,Body,Description,Photo,Meta1,Meta2,ViewCount,SeoTitle,SeoDescription,SeoKeywords,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", post.LanguageId);
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
    }
}
