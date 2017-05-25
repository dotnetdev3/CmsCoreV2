using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using SaasKit.Multitenancy;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("Cmscore")]
    public class SettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;      
        private IHostingEnvironment env;
        protected readonly AppTenant tenant;
        public SettingsController(IHostingEnvironment _env, ITenant<AppTenant> tenant, ApplicationDbContext context)
        {
            _context = context;
            this.env = _env;
            this.tenant = tenant?.Value;
        }

        // GET: CmsCore/Settings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }
        public async Task<IActionResult> Mail()
        {
            return View(await _context.Settings.ToListAsync());
        }
        // GET: CmsCore/Settings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: CmsCore/Settings/Create
        public IActionResult Create()
        {
            var setting = new Setting();
            return View(setting);
        }

        // POST: CmsCore/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeaderString,GoogleAnalytics,FooterScript,MapLat,MapLon,SmtpUserName,SmtpPassword,SmtpHost,SmtpPort,SmtpUseSSL,Name,Value,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                setting.CreatedBy = User.Identity.Name ?? "username";
                setting.CreateDate = DateTime.Now;
                setting.UpdatedBy = User.Identity.Name ?? "username";
                setting.UpdateDate = DateTime.Now;
                setting.AppTenantId = tenant.AppTenantId;
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: CmsCore/Settings/Edit/5
        public async Task<IActionResult> Edit()
        {

         

            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.AppTenantId == tenant.AppTenantId);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: CmsCore/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("HeaderString,GoogleAnalytics,FooterScript,MapLat,MapLon,SmtpUserName,SmtpPassword,SmtpHost,SmtpPort,SmtpUseSSL,Name,Value,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Setting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    setting.UpdatedBy = User.Identity.Name ?? "username";
                    setting.UpdateDate = DateTime.Now;
                    setting.AppTenantId = tenant.AppTenantId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
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
            return View(setting);
        }

        // GET: CmsCore/Settings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: CmsCore/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.Id == id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SettingExists(long id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}
