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
    public class GalleryItemsController : ControllerBase
    {
        private IHostingEnvironment env;

        public GalleryItemsController(IHostingEnvironment _env, ITenant<AppTenant> tenant, ApplicationDbContext context) : base(context, tenant)
        {
            this.env = _env;
        }

        // GET: CmsCore/GalleryItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SetFiltered<GalleryItem>().Where(x => x.AppTenantId == tenant.AppTenantId).Include(g => g.Gallery);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/GalleryItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems
                .Include(g => g.Gallery)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            return View(galleryItem);
        }

        // GET: CmsCore/GalleryItems/Create
        public IActionResult Create()
        {

            var galleryItem = new GalleryItem();
            galleryItem.CreatedBy = User.Identity.Name ?? "username";
            galleryItem.CreateDate = DateTime.Now;
            galleryItem.UpdatedBy = User.Identity.Name ?? "username";
            galleryItem.UpdateDate = DateTime.Now;
            ViewData["GalleryId"] = new SelectList(_context.Galleries.ToList(), "Id", "Name",galleryItem.GalleryId);
            ViewBag.CategoryList = GetGalleryItemCategories();
            return View(galleryItem);
        }

        // POST: CmsCore/GalleryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Position,Photo,Video,Meta1,GalleryId,IsPublished,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] GalleryItem galleryItem)
        {
            if (ModelState.IsValid)
            {
                galleryItem.CreatedBy = User.Identity.Name ?? "username";
                galleryItem.CreateDate = DateTime.Now;
                galleryItem.UpdatedBy = User.Identity.Name ?? "username";
                galleryItem.UpdateDate = DateTime.Now;
                galleryItem.AppTenantId = tenant.AppTenantId;
                _context.Add(galleryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GalleryId"] = new SelectList(_context.Galleries.ToList(), "Id", "Name", galleryItem.GalleryId);
            ViewBag.CategoryList = GetGalleryItemCategories();
            return View(galleryItem);
        }

        // GET: CmsCore/GalleryItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems.SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItem == null)
            {
                return NotFound();
            }
            ViewData["GalleryId"] = new SelectList(_context.Galleries.ToList(), "Id", "Id", galleryItem.GalleryId);
            ViewBag.CategoryList = GetGalleryItemCategories();
            return View(galleryItem);
        }

        // POST: CmsCore/GalleryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,Description,Position,Photo,Video,Meta1,GalleryId,IsPublished,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] GalleryItem galleryItem)
        {
            if (id != galleryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    galleryItem.UpdatedBy = User.Identity.Name ?? "username";
                    galleryItem.UpdateDate = DateTime.Now;
                    galleryItem.AppTenantId = tenant.AppTenantId;
                    _context.Update(galleryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryItemExists(galleryItem.Id))
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
            ViewData["GalleryId"] = new SelectList(_context.Galleries.ToList(), "Id", "Id", galleryItem.GalleryId);
            ViewBag.CategoryList = GetGalleryItemCategories();
            return View(galleryItem);
        }

        // GET: CmsCore/GalleryItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems
                .Include(g => g.Gallery)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            return View(galleryItem);
        }

        // POST: CmsCore/GalleryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var galleryItem = await _context.GalleryItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.GalleryItems.Remove(galleryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GalleryItemExists(long id)
        {
            return _context.GalleryItems.Any(e => e.Id == id);
        }
        public IEnumerable<GalleryItemCategory> GetGalleryItemCategories()
        {
            var galleryItemCategories = _context.GalleryItemCategories.AsQueryable().Include("ChildCategories").ToList(); ;
            return galleryItemCategories;

        }
    }
}
