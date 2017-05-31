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
    public class CustomizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppTenant tenant;

        public CustomizationsController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;
        }


        public async Task<IActionResult> Edit()
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;

            

            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            if (customization == null)
            {
                return NotFound();
            }
            return View(customization);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customization customization)
        {

            if (customization == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customization.AppTenantId = tenant.AppTenantId;
                    customization.ThemeId = tenant.ThemeId;
                    customization.UpdateDate = DateTime.Now;
                    _context.Update(customization);
                    await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomizationExists(customization.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                ViewData["var"] = "islem basarýlý !";
                return RedirectToAction("Edit");
            }
            return View(customization);
        }
        public async Task<IActionResult> CustomCss()
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;



            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            if (customization == null)
            {
                return NotFound();
            }
            return View(customization);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomCss(Customization customization)
        {

            if (customization == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customization.AppTenantId = tenant.AppTenantId;
                    customization.ThemeId = tenant.ThemeId;
                    customization.UpdateDate = DateTime.Now;
                    _context.Update(customization);
                    await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomizationExists(customization.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                ViewData["var"] = "islem basarýlý";
                return RedirectToAction("CustomCss");
            }
            return View(customization);
        }



        private bool CustomizationExists(long id)
        {
            return _context.Customizations.Any(e => e.Id == id);
        }
    }
}

