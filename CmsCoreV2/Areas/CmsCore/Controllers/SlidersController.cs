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
using Microsoft.AspNetCore.Authorization;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Authorize(Roles = "SLIDE,ADMIN")]
    [Area("CmsCore")]
    public class SlidersController : ControllerBase
    {

        
        public SlidersController(ITenant<AppTenant> tenant, ApplicationDbContext context) : base(context, tenant)
        {

        }

        // GET: CmsCore/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.SetFiltered<Slider>().Where(x => x.AppTenantId == tenant.AppTenantId).ToListAsync());
        }

        // GET: CmsCore/Sliders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: CmsCore/Sliders/Create
        public IActionResult Create()
        {
            var slider = new Slider();
            slider.CreatedBy = User.Identity.Name ?? "username";
            slider.CreateDate = DateTime.Now;
            slider.UpdatedBy = User.Identity.Name ?? "username";
            slider.UpdateDate = DateTime.Now;
            slider.AppTenantId = tenant.AppTenantId;
            return View(slider);
       
        }

        // POST: CmsCore/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsPublished,Template,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.CreatedBy = User.Identity.Name ?? "username";
                slider.CreateDate = DateTime.Now;
                slider.UpdatedBy = User.Identity.Name ?? "username";
                slider.UpdateDate = DateTime.Now;
                slider.AppTenantId = tenant.AppTenantId;
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: CmsCore/Sliders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.SingleOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: CmsCore/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,IsPublished,Template,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Slider slider)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
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
            return View(slider);
        }

        // GET: CmsCore/Sliders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: CmsCore/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var slider = await _context.Sliders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SliderExists(long id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
