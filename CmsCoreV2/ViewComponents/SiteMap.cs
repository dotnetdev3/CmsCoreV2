using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsCoreV2.ViewComponents
{
    public class SiteMap : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public SiteMap(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? parentPageId)
        {
            var items = await GetItems(parentPageId);
            return View(items);
        }

        private async Task<List<Page>> GetItems(long? parentPageId)
        {
            List<Page> pages = context.Pages.Include("Language").Where(p => p.ParentPageId == parentPageId).ToList<Page>();
            return await Task.FromResult(pages);
        }
    }
}
