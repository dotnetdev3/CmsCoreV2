using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Data;
using CmsCoreV2.Models;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class SlidesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlidesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CmsCore/Slides
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Slides.Include(s => s.Slider);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/Slides/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .Include(s => s.Slider)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // GET: CmsCore/Slides/Create
        public IActionResult Create()
        {
            ViewData["SliderId"] = new SelectList(_context.Sliders, "Id", "Id");
            return View();
        }

        // POST: CmsCore/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,SubTitle,Description,Position,Photo,Video,CallToActionText,CallToActionUrl,DisplayTexts,IsPublished,SliderId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slide);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SliderId"] = new SelectList(_context.Sliders, "Id", "Id", slide.SliderId);
            return View(slide);
        }

        // GET: CmsCore/Slides/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides.SingleOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }
            ViewData["SliderId"] = new SelectList(_context.Sliders, "Id", "Id", slide.SliderId);
            return View(slide);
        }

        // POST: CmsCore/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,SubTitle,Description,Position,Photo,Video,CallToActionText,CallToActionUrl,DisplayTexts,IsPublished,SliderId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Slide slide)
        {
            if (id != slide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideExists(slide.Id))
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
            ViewData["SliderId"] = new SelectList(_context.Sliders, "Id", "Id", slide.SliderId);
            return View(slide);
        }

        // GET: CmsCore/Slides/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .Include(s => s.Slider)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // POST: CmsCore/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var slide = await _context.Slides.SingleOrDefaultAsync(m => m.Id == id);
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SlideExists(long id)
        {
            return _context.Slides.Any(e => e.Id == id);
        }
    }
}
