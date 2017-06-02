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
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;
using System.Linq.Expressions;
using CmsCoreV2.Services;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    
    [Area("CmsCore")]
    public class FeedbacksController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;
        private readonly IFeedbackService feedbackService;
        public FeedbacksController(ApplicationDbContext context, ITenant<AppTenant> tenant, IFeedbackService feedbackService)
        {
            _context = context;
            this.tenant = tenant?.Value;
            this.feedbackService = feedbackService;
        }
        // GET: CmsCore/Feedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feedbacks.ToListAsync());
        }

        // GET: CmsCore/Feedbacks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.AsQueryable().Include("FeedbackValues")
                .SingleOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewBag.FeedbackValues = feedback.FeedbackValues.ToList();
            return View(feedback);
        }

        // GET: CmsCore/Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CmsCore/Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection formCollection)
        {

            feedbackService.FeedbackPost(formCollection, Request.HttpContext.Connection.RemoteIpAddress.ToString(), tenant.AppTenantId);

            return RedirectToAction("Index");
        }

        // GET: CmsCore/Feedbacks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.SingleOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: CmsCore/Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserName,SentDate,FormId,FormName,IP,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Feedback feedback, FeedbackValue feedbackValue)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }
            feedback.UpdatedBy = User.Identity.Name ?? "username";
            feedback.UpdateDate = DateTime.Now;
            feedback.AppTenantId = tenant.AppTenantId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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
            return View(feedback);
        }

        // GET: CmsCore/Feedbacks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: CmsCore/Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            
            var feedbackValue = await _context.FeedbackValues.SingleOrDefaultAsync(m => m.FeedbackId == id);

            _context.FeedbackValues.Remove(feedbackValue);
            await _context.SaveChangesAsync();
            var feedback = await _context.Feedbacks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FeedbackExists(long id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
