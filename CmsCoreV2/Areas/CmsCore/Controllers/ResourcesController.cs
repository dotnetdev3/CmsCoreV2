using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Hosting;
using SaasKit.Multitenancy;
using Z.EntityFramework.Plus;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class ResourcesController : ControllerBase
    {
        private IHostingEnvironment env;

        public ResourcesController(IHostingEnvironment _env, ITenant<AppTenant> tenant, ApplicationDbContext context) : base(context, tenant)
        {
            this.env = _env;
        }

        // GET: CmsCore/Resources
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<Resource>().Where(x => x.AppTenantId == tenant.AppTenantId).Include(r => r.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/Resources/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .Include(r => r.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: CmsCore/Resources/Create
        public IActionResult Create( )
        {
            Resource resource = new Resource();
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", resource.LanguageId);
            return View();
        }

        // POST: CmsCore/Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Value,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName");
                resource.CreateDate = DateTime.Now;
                resource.CreatedBy = User.Identity.Name ?? "username";
                resource.UpdateDate = DateTime.Now;
                resource.UpdatedBy = User.Identity.Name ?? "username";
                resource.AppTenantId = tenant.AppTenantId;
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", resource.LanguageId);
            return View(resource);
        }

        // GET: CmsCore/Resources/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", resource.LanguageId);
            return View(resource);
        }

        // POST: CmsCore/Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Value,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Resource resource)
        {
            if (id != resource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    resource.UpdateDate = DateTime.Now;
                    resource.UpdatedBy = User.Identity.Name ?? "username";
                    resource.AppTenantId = tenant.AppTenantId;
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", resource.LanguageId);
            return View(resource);
        }

        // GET: CmsCore/Resources/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .Include(r => r.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // POST: CmsCore/Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.Id == id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ResourceExists(long id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
