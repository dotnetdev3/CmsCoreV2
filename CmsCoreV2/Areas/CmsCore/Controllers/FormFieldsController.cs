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
    public class FormFieldsController : Controller
    {
        private readonly ApplicationDbContext _context;

        protected readonly AppTenant tenant;

        public FormFieldsController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;
        }
        // GET: CmsCore/FormFields
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FormFields.Include(f => f.Form);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CmsCore/FormFields/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields
                .Include(f => f.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (formField == null)
            {
                return NotFound();
            }

            return View(formField);
        }

        // GET: CmsCore/FormFields/Create
        public IActionResult Create()
        {
            ViewData["FormId"] = new SelectList(_context.Forms.ToList(), "Id", "FormName");
            return View();
        }

        // POST: CmsCore/FormFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Required,Value,Position,FieldType,FormId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] FormField formField)
        {
            formField.CreatedBy = User.Identity.Name ?? "username";
            formField.CreateDate = DateTime.Now;
            formField.UpdatedBy = User.Identity.Name ?? "username";
            formField.UpdateDate = DateTime.Now;
            formField.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                _context.Add(formField);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FormId"] = new SelectList(_context.Forms.ToList(), "Id", "FormName", formField.FormId);
            return View(formField);
        }

        // GET: CmsCore/FormFields/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields.SingleOrDefaultAsync(m => m.Id == id);
            if (formField == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms.ToList(), "Id", "FormName", formField.FormId);
            return View(formField);
        }

        // POST: CmsCore/FormFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Required,Value,Position,FieldType,FormId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] FormField formField)
        {
            if (id != formField.Id)
            {
                return NotFound();
            }
            formField.UpdatedBy = User.Identity.Name ?? "username";
            formField.UpdateDate = DateTime.Now;
            formField.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormFieldExists(formField.Id))
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
            ViewData["FormId"] = new SelectList(_context.Forms.ToList(), "Id", "FormName", formField.FormId);
            return View(formField);
        }

        // GET: CmsCore/FormFields/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formField = await _context.FormFields
                .Include(f => f.Form)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (formField == null)
            {
                return NotFound();
            }

            return View(formField);
        }

        // POST: CmsCore/FormFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var formField = await _context.FormFields.SingleOrDefaultAsync(m => m.Id == id);
            _context.FormFields.Remove(formField);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FormFieldExists(long id)
        {
            return _context.FormFields.Any(e => e.Id == id);
        }
    }
}
