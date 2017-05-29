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
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;

        public FormsController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;
        }

        // GET: CmsCore/Forms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Forms.Include(f => f.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Preview(long? id)
        {
            var form = await _context.Forms.Include("FormFields").SingleOrDefaultAsync(m => m.Id == id);
            return View(form);
        }

        // GET: CmsCore/Forms/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: CmsCore/Forms/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "Id");
            return View();
        }

        // POST: CmsCore/Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormName,EmailTo,EmailBcc,EmailCc,Description,Template,ClosingDescription,GoogleAnalyticsCode,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Form form)
        {
            form.CreatedBy = User.Identity.Name ?? "username";
            form.CreateDate = DateTime.Now;
            form.UpdatedBy = User.Identity.Name ?? "username";
            form.UpdateDate = DateTime.Now;
            form.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "Id", form.LanguageId);
            return View(form);
        }

        // GET: CmsCore/Forms/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms.SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "Id", form.LanguageId);
            return View(form);
        }

        // POST: CmsCore/Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FormName,EmailTo,EmailBcc,EmailCc,Description,Template,ClosingDescription,GoogleAnalyticsCode,IsPublished,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }
            form.UpdatedBy = User.Identity.Name ?? "username";
            form.UpdateDate = DateTime.Now;
            form.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "Id", form.LanguageId);
            return View(form);
        }

        // GET: CmsCore/Forms/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: CmsCore/Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var form = await _context.Forms.SingleOrDefaultAsync(m => m.Id == id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FormExists(long id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}
