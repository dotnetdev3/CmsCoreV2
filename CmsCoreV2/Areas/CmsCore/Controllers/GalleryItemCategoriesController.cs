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
    [Authorize(Roles = "ADMIN,GALLERY")]
    [Area("CmsCore")]
    public class GalleryItemCategoriesController : ControllerBase
    {
        private IHostingEnvironment env;

        public GalleryItemCategoriesController(IHostingEnvironment _env, ITenant<AppTenant> tenant, ApplicationDbContext context) : base(context, tenant)
        { 
            this.env = _env;
        }

        // GET: CmsCore/GalleryItemCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<GalleryItemCategory>().Where(x => x.AppTenantId == tenant.AppTenantId).Include(g => g.ParentCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/GalleryItemCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItemCategory = await _context.GalleryItemCategories
                .Include(g => g.ParentCategory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItemCategory == null)
            {
                return NotFound();
            }

            return View(galleryItemCategory);
        }

        // GET: CmsCore/GalleryItemCategories/Create
        public IActionResult Create()
        {
            
            var galleryItemCategory = new GalleryItemCategory();

            var parentCategory = _context.GalleryItemCategories.ToList();
            var result = "";
            recurseGalleryItem(ref parentCategory, null, 0, ref result);
            ViewBag.ParentCategory = result;


            galleryItemCategory.CreatedBy = User.Identity.Name ?? "username";
            galleryItemCategory.CreateDate = DateTime.Now;
            galleryItemCategory.UpdatedBy = User.Identity.Name ?? "username";
            galleryItemCategory.UpdateDate = DateTime.Now;
            galleryItemCategory.AppTenantId = tenant.AppTenantId;
            return View(galleryItemCategory);
        }

        static void recurseGalleryItem(ref List<GalleryItemCategory> cl, GalleryItemCategory start, int level, ref string result)
        {
            foreach (GalleryItemCategory child in cl)
            {
                if (child.ParentCategory == start)
                {
                    result += "<option value='" + child.Id.ToString() + "'>" + (new String(' ', level * 2)).Replace(" ", "&nbsp") + child.Name + "</option>";
                    recurseGalleryItem(ref cl, child, level + 1, ref result);
                }
            }

        }

        // POST: CmsCore/GalleryItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,Description,ParentCategoryId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] GalleryItemCategory galleryItemCategory)
        {
            if (ModelState.IsValid)
            {
                galleryItemCategory.CreatedBy = User.Identity.Name ?? "username";
                galleryItemCategory.CreateDate = DateTime.Now;
                galleryItemCategory.UpdatedBy = User.Identity.Name ?? "username";
                galleryItemCategory.UpdateDate = DateTime.Now;
                galleryItemCategory.AppTenantId = tenant.AppTenantId;
                _context.Add(galleryItemCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.GalleryItemCategories.ToList(), "Id", "Id", galleryItemCategory.ParentCategoryId);
            return View(galleryItemCategory);
        }

        // GET: CmsCore/GalleryItemCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItemCategory = await _context.GalleryItemCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItemCategory == null)
            {
                return NotFound();
            }
            var parentCategory = _context.GalleryItemCategories.ToList();
            var result = "";
            recurseGalleryItem(ref parentCategory, null, 0, ref result);
            ViewBag.ParentCategory = result;
            return View(galleryItemCategory);
        }

        // POST: CmsCore/GalleryItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Slug,Description,ParentCategoryId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] GalleryItemCategory galleryItemCategory)
        {
            if (id != galleryItemCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    galleryItemCategory.UpdatedBy = User.Identity.Name ?? "username";
                    galleryItemCategory.UpdateDate = DateTime.Now;
                    galleryItemCategory.AppTenantId = tenant.AppTenantId;
                    _context.Update(galleryItemCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryItemCategoryExists(galleryItemCategory.Id))
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
            ViewData["ParentCategoryId"] = new SelectList(_context.GalleryItemCategories.ToList(), "Id", "Id", galleryItemCategory.ParentCategoryId);
            return View(galleryItemCategory);
        }

        // GET: CmsCore/GalleryItemCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItemCategory = await _context.GalleryItemCategories
                .Include(g => g.ParentCategory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItemCategory == null)
            {
                return NotFound();
            }

            return View(galleryItemCategory);
        }

        // POST: CmsCore/GalleryItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var galleryItemCategory = await _context.GalleryItemCategories.SingleOrDefaultAsync(m => m.Id == id);
            _context.GalleryItemCategories.Remove(galleryItemCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryItemCategoryExists(long id)
        {
            return _context.GalleryItemCategories.Any(e => e.Id == id);
        }
    }
}
