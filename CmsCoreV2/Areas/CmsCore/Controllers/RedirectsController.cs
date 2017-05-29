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
    public class RedirectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppTenant tenant;
        public RedirectsController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;  
            this.tenant = tenant?.Value;
        }

        // GET: CmsCore/Redirects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Redirects.ToListAsync());
        }

        // GET: CmsCore/Redirects/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var redirect = await _context.Redirects
                .SingleOrDefaultAsync(m => m.Id == id);
            redirect.CreateDate = DateTime.Now;
            if (redirect == null)
            {
                return NotFound();
            }

            return View(redirect);
        }

        // GET: CmsCore/Redirects/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: CmsCore/Redirects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,OldUrl,NewUrl,IsActive,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Redirect redirect)
        {
            redirect.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(redirect);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(redirect);
        }

        // GET: CmsCore/Redirects/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var redirect = await _context.Redirects.SingleOrDefaultAsync(m => m.Id == id);
            redirect.CreateDate = DateTime.Now;
            if (redirect == null)
            {
                return NotFound();
            }
            return View(redirect);
        }

        // POST: CmsCore/Redirects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,OldUrl,NewUrl,IsActive,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Redirect redirect)
        {
            if (id != redirect.Id)
            {
                return NotFound();
            }
            redirect.UpdateDate = DateTime.Now; 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redirect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedirectExists(redirect.Id))
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
            return View(redirect);
        }

        // GET: CmsCore/Redirects/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var redirect = await _context.Redirects
                .SingleOrDefaultAsync(m => m.Id == id);
            if (redirect == null)
            {
                return NotFound();
            }

            return View(redirect);
        }

        // POST: CmsCore/Redirects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var redirect = await _context.Redirects.SingleOrDefaultAsync(m => m.Id == id);
            _context.Redirects.Remove(redirect);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RedirectExists(long id)
        {
            return _context.Redirects.Any(e => e.Id == id);
        }
    }
}
