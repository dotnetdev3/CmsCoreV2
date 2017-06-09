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
using CmsCoreV2.Models.ViewModels;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class CustomizationsController : ControllerBase
    {


        public CustomizationsController(ApplicationDbContext context, ITenant<AppTenant> tenant) : base(context, tenant)
        {
            
        }


        public async Task<IActionResult> Edit()
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;
            
            CustomizationViewModel customizationViewModel = new CustomizationViewModel();

            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            if (customization == null)
            {
                return NotFound();
            }
            customizationViewModel.ThemeName = customization.ThemeName;
            customizationViewModel.ComponentTemplates = customization.ComponentTemplates;
            customizationViewModel.PageTemplates = customization.PageTemplates;
            customizationViewModel.MetaDescription = customization.MetaDescription;
            customizationViewModel.MetaKeywords = customization.MetaKeywords;
            customizationViewModel.MetaTitle = customization.MetaTitle;
            customizationViewModel.Logo = customization.Logo;
            
            customizationViewModel.ManyLocations = customization.ManyLocations;
            customizationViewModel.UpdateDate = customization.UpdateDate;
            customizationViewModel.CreateDate = customization.CreateDate;
            return View(customizationViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomizationViewModel customizationViewModel)
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;
            if (customizationViewModel == null)
            {
                return NotFound();
            }
            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            if (ModelState.IsValid)
            {
                try
                {
                    
                    customization.UpdateDate = DateTime.Now;
                    customization.ThemeName = customizationViewModel.ThemeName;
                    customization.Logo = customizationViewModel.Logo;
                    customization.ManyLocations = customizationViewModel.ManyLocations;
                    customization.MetaDescription = customizationViewModel.MetaDescription;
                    customization.MetaKeywords = customizationViewModel.MetaKeywords;
                    customization.PageTemplates = customizationViewModel.PageTemplates;
                    customization.MetaTitle = customizationViewModel.MetaTitle;
                    
                    customization.ComponentTemplates = customizationViewModel.ComponentTemplates;
                    customization.PageTemplates = customizationViewModel.PageTemplates;
                    customization.CreateDate = customizationViewModel.CreateDate;
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
            return View(customizationViewModel);
        }
        public async Task<IActionResult> CustomCss()
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;

            CustomCssViewModel customCssViewModel = new CustomCssViewModel();

            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            customCssViewModel.ThemeName = customization.ThemeName;
            customCssViewModel.CustomCSS = customization.CustomCSS;
            customCssViewModel.UpdateDate = customization.UpdateDate;
            customCssViewModel.CreateDate = customization.CreateDate;

            if (customization == null)
            {
                return NotFound();
            }
            return View(customCssViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomCss(CustomCssViewModel customCssViewModel)
        {
            var _tenantCustomizationThemeId = tenant.ThemeId;

            

            var customization = await _context.Customizations.SingleOrDefaultAsync(c => c.ThemeId == _tenantCustomizationThemeId);
            if (customization == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customization.ThemeName = customCssViewModel.ThemeName;
                    customization.CustomCSS = customCssViewModel.CustomCSS;
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
            return View(customCssViewModel);
        }



        private bool CustomizationExists(long id)
        {
            return _context.Customizations.Any(e => e.Id == id);
        }
    }
}

