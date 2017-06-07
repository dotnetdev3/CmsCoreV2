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
    [Authorize(Roles = "ADMIN,PAGE")]
    [Area("CmsCore")]
    public class PagesController : ControllerBase
    {
      
        public PagesController(ApplicationDbContext context, ITenant<AppTenant> tenant) : base(context, tenant)
        {

        }

        // GET: CmsCore/Pages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<Page>().Where(x=>x.AppTenantId==tenant.AppTenantId).Include(p => p.Language).Include(p => p.ParentPage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/Pages/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .Include(p => p.Language)
                .Include(p => p.ParentPage)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: CmsCore/Pages/Create
        
        public IActionResult Create()
        {
            var page = new Page();
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName");
            ViewData["ParentPageId"] = new SelectList(_context.Pages.ToList(), "Id", "Title");
            page.CreatedBy = User.Identity.Name ?? "username";
            page.CreateDate = DateTime.Now;
            page.UpdatedBy = User.Identity.Name ?? "username";
            page.UpdateDate = DateTime.Now;
            page.AppTenantId = tenant.AppTenantId;
            return View(page);
        }

        // POST: CmsCore/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Slug,Body,ViewCount,ParentPageId,SeoTitle,SeoDescription,SeoKeywords,IsPublished,Template,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Page page)
        {


            if (ModelState.IsValid)
            {
                page.CreatedBy = User.Identity.Name ?? "username";
                page.CreateDate = DateTime.Now;
                page.UpdatedBy = User.Identity.Name ?? "username";
                page.UpdateDate = DateTime.Now;
                page.AppTenantId = tenant.AppTenantId;

                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", page.LanguageId);
            ViewData["ParentPageId"] = new SelectList(_context.Pages.ToList(), "Id", "Title", page.ParentPageId);
            return View(page);
        }

        // GET: CmsCore/Pages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", page.LanguageId);
            ViewData["ParentPageId"] = new SelectList(_context.Pages.ToList(), "Id", "Title", page.ParentPageId);

            page.UpdatedBy = User.Identity.Name ?? "username";
            page.UpdateDate = DateTime.Now;
            page.AppTenantId = tenant.AppTenantId;

            return View(page);
        }

        // POST: CmsCore/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,Slug,Body,ViewCount,ParentPageId,SeoTitle,SeoDescription,SeoKeywords,IsPublished,Template,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                page.UpdatedBy = User.Identity.Name ?? "username";
                page.UpdateDate = DateTime.Now;
                page.AppTenantId = tenant.AppTenantId;
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", page.LanguageId);
            ViewData["ParentPageId"] = new SelectList(_context.Pages.ToList(), "Id", "Title", page.ParentPageId);
            return View(page);
        }

        // GET: CmsCore/Pages/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .Include(p => p.Language)
                .Include(p => p.ParentPage)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: CmsCore/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PageExists(long id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
