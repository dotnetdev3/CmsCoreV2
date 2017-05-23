using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Models;
using CmsCoreV2.Data;
using CmsCoreV2.Data.Migrations;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCoreV2")]
    public class SlidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlidersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CmsCore/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slider.ToListAsync());
        }

        // GET: CmsCore/Sliders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
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
            return View();
        }

        // POST: CmsCore/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsPublished,Template,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] slider slider)
        {
            if (ModelState.IsValid)
            {
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

            var slider = await _context.Slider.SingleOrDefaultAsync(m => m.Id == id);
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

            var slider = await _context.Slider
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
            var slider = await _context.Slider.SingleOrDefaultAsync(m => m.Id == id);
            _context.Slider.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SliderExists(long id)
        {
            return _context.Slider.Any(e => e.Id == id);
        }
    }
}
