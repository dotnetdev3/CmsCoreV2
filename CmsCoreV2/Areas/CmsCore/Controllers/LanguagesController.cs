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

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class LanguagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment env;
        protected readonly AppTenant tenant;


        public LanguagesController(IHostingEnvironment _env, ITenant<AppTenant> tenant, ApplicationDbContext context)
        {
            _context = context;
            this.env = _env;
            this.tenant = tenant?.Value;
        }



        // GET: CmsCore/Languages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Languages.ToListAsync());
        }

        // GET: CmsCore/Languages/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .SingleOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: CmsCore/Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CmsCore/Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NativeName,Culture,IsActive,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Language language)
        {
            if (ModelState.IsValid)
            {
                language.CreateDate = DateTime.Now;
                language.CreatedBy = User.Identity.Name ?? "username";
                language.UpdateDate = DateTime.Now;
                language.UpdatedBy = User.Identity.Name ?? "username";
                language.AppTenantId = tenant.AppTenantId;

                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: CmsCore/Languages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.SingleOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: CmsCore/Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,NativeName,Culture,IsActive,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Language language)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    language.UpdateDate = DateTime.Now;
                    language.UpdatedBy = User.Identity.Name ?? "username";
                    language.AppTenantId = tenant.AppTenantId;

                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
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
            return View(language);
        }

        // GET: CmsCore/Languages/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .SingleOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: CmsCore/Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var language = await _context.Languages.SingleOrDefaultAsync(m => m.Id == id);
            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LanguageExists(long id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
