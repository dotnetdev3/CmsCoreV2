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

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class MenusController : ControllerBase
    {

        public MenusController(ApplicationDbContext context, ITenant<AppTenant> tenant) : base(context, tenant)
        { 

        }

        // GET: CmsCore/Menus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<Menu>().Where(x => x.AppTenantId == tenant.AppTenantId).Include(m => m.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/Menus/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: CmsCore/Menus/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName");
            return View();
        }

        // POST: CmsCore/Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MenuLocation,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Menu menu)
        {
            menu.CreatedBy = User.Identity.Name ?? "username";
            menu.CreateDate = DateTime.Now;
            menu.UpdatedBy = User.Identity.Name ?? "username";
            menu.UpdateDate = DateTime.Now;
            menu.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
               
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", menu.LanguageId);
            return View(menu);
        }

        // GET: CmsCore/Menus/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", menu.LanguageId);
            return View(menu);
        }

        // POST: CmsCore/Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,MenuLocation,LanguageId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }
            menu.UpdatedBy = User.Identity.Name ?? "username";
            menu.UpdateDate = DateTime.Now;
            menu.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages.ToList(), "Id", "NativeName", menu.LanguageId);
            return View(menu);
        }

        // GET: CmsCore/Menus/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Language)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: CmsCore/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuExists(long id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
