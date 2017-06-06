using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaasKit.Multitenancy;
using CmsCoreV2.Models;
using CmsCoreV2.Data;
using Microsoft.AspNetCore.Authorization;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Authorize]
    [Area("CmsCore")]

    public class DashboardController: ControllerBase
    {
        public DashboardController( ITenant<AppTenant> tenant, ApplicationDbContext context) : base(context, tenant)
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
