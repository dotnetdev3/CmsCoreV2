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
    public class PostCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;


        public PostCategoriesController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;

        }

        // GET: CmsCore/PostCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostCategories.Include(p => p.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/PostCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .Include(p => p.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: CmsCore/PostCategories/Create
        public IActionResult Create()
        {
            var postCategory = new PostCategory();
            postCategory.CreatedBy = User.Identity.Name ?? "username";
            postCategory.CreateDate = DateTime.Now;
            postCategory.UpdatedBy = User.Identity.Name ?? "username";
            postCategory.UpdateDate = DateTime.Now;
            postCategory.AppTenantId = tenant.AppTenantId;

            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture");
            return View(postCategory);
        }

        // POST: CmsCore/PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,Description,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                postCategory.CreatedBy = User.Identity.Name ?? "username";
                postCategory.CreateDate = DateTime.Now;
                postCategory.UpdatedBy = User.Identity.Name ?? "username";
                postCategory.UpdateDate = DateTime.Now;
                postCategory.AppTenantId = tenant.AppTenantId;

                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", postCategory.LanguageId);
            return View(postCategory);
        }

        // GET: CmsCore/PostCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", postCategory.LanguageId);

            postCategory.UpdatedBy = User.Identity.Name ?? "username";
            postCategory.UpdateDate = DateTime.Now;
            postCategory.AppTenantId = tenant.AppTenantId;

            return View(postCategory);
        }

        // POST: CmsCore/PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Slug,Description,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] PostCategory postCategory)
        {
            if (id != postCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    postCategory.UpdatedBy = User.Identity.Name ?? "username";
                    postCategory.UpdateDate = DateTime.Now;
                    postCategory.AppTenantId = tenant.AppTenantId;

                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Culture", postCategory.LanguageId);
            

            return View(postCategory);
        }

        // GET: CmsCore/PostCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .Include(p => p.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: CmsCore/PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var postCategory = await _context.PostCategories.SingleOrDefaultAsync(m => m.Id == id);
            _context.PostCategories.Remove(postCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostCategoryExists(long id)
        {
            return _context.PostCategories.Any(e => e.Id == id);
        }
    }
}
