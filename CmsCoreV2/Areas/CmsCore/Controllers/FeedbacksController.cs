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

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    
    [Area("CmsCore")]
    public class FeedbacksController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        protected readonly AppTenant tenant;
        public FeedbacksController(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            _context = context;
            this.tenant = tenant?.Value;
        }
        // GET: CmsCore/Feedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feedbacks.ToListAsync());
        }

        [HttpPost]
        public IActionResult PostForm(IFormCollection formCollection)
        {
            FeedbackPost(formCollection, null);
            return RedirectToAction("Successful");
        }

        public virtual IEnumerable<Form> GetAll(params string[] navigations)
        {
            var set = _context.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.AsEnumerable();
        }

        public virtual Form GetById(long id, params string[] navigations)
        {
            var set = _context.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Form> GetForms()
        {
            var forms = GetAll(); // içerde FormField yazýyordu
            return forms;
        }

        public Form GetForm(long id)
        {
            var form = GetById(id, "FormFields");
            return form;
        }

        public Form Get(Expression<Func<Form, bool>> where, params string[] navigations)
        {
            var set = _context.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.Where(where).FirstOrDefault<Form>();
        }

        public Form GetForm(string name)
        {
            name = name.ToLower();
            var form = Get(f => f.FormName.ToLower() == name, "FormFields");
            return form;
        }

        public List<FormField> GetFormFieldsByFormId(long id)
        {
            Form form = GetById(id, "FormFields");
            return form.FormFields.OrderBy(c => c.Position).ToList();
        }

        public void FeedbackPost(IFormCollection collection, string filePath)
        {
            int i = 3;
            Feedback feed_back = new Feedback();
            var form = GetForm(Convert.ToInt64(collection["Id"]));
            var body = "";
            foreach (var item in GetFormFieldsByFormId(Convert.ToInt64(collection["Id"])))
            {
                var feedBackValue = new FeedbackValue();

                feedBackValue.FormFieldName = item.Name;
                feedBackValue.FieldType = item.FieldType;
                feedBackValue.FormFieldId = (int)item.Id;
                feedBackValue.Position = item.Position;
                feedBackValue.CreatedBy = "username";
                feedBackValue.CreateDate = DateTime.Now;
                feedBackValue.UpdatedBy = "username";
                feedBackValue.UpdateDate = DateTime.Now;
                
                foreach (var item2 in collection)
                {
                    if (item.Name == item2.Key)
                    {
                        feedBackValue.Value = item2.Value;
                        body = body + item2.Key + " : " + item2.Value + "<br/>";
                    }
                }

                feed_back.FeedbackValues.Add(feedBackValue);
            }
            //feedBack.IP = GetUserIP();  // gönderen ip method u eklenecek

            //var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;


            feed_back.FormId = (int)form.Id;
            feed_back.FormName = form.FormName;

            //feed_back.IP = remoteIpAddress.ToString();
            feed_back.SentDate = DateTime.Now;
            feed_back.UserName = "username";
            feed_back.CreatedBy = "username";
            feed_back.CreateDate = DateTime.Now;
            feed_back.UpdateDate = DateTime.Now;
            feed_back.UpdatedBy = "username";
            feed_back.IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            CreateFeedback(feed_back);
            SaveFeedback();
            body = body + "<br>" + "Gönderilme Tarihi : " + DateTime.Now;
            FeedbackPostMail(body, form.Id);
            //return feed_back.FeedbackValues.ToList();

        }

        public void CreateFeedback(Feedback Feedback)
        {
            _context.Add(Feedback);
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void SaveFeedback()
        {
            Commit();
        }

        

        public void FeedbackPostMail(string body, long id)
        {
            var form = GetForm(id);
            if (form.EmailBcc != null || form.EmailCc != null || form.EmailTo != null)
            {
                var message = new MimeMessage();
                if (form.EmailCc != null)
                {
                    var email_cc_list = form.EmailCc.Split(',');
                    foreach (var item2 in email_cc_list)
                    {
                        message.Cc.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                if (form.EmailBcc != null)
                {
                    var email_bcc_list = form.EmailBcc.Split(',');
                    foreach (var item2 in email_bcc_list)
                    {
                        message.Bcc.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                if (form.EmailTo != null)
                {
                    var email_to_list = form.EmailTo.Split(',');
                    foreach (var item2 in email_to_list)
                    {
                        message.To.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                var setting = _context.Settings.FirstOrDefault();
                message.From.Add(new MailboxAddress("CMS Core", setting.SmtpUserName));
                var bodyBuilder = new BodyBuilder();
                message.Subject = "CMS Core " + form.FormName;
                // foreach (var item in feed_back.FeedbackValues)
                //{
                //message.Body += EmailString(item).ToString() + "<br/>";
                bodyBuilder.HtmlBody += body;
                //}
                message.Body = bodyBuilder.ToMessageBody();
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.
                        client.Authenticate(setting.SmtpUserName, setting.SmtpPassword);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // GET: CmsCore/Feedbacks/Details/5
        public async Task<IActionResult> Details(long? id)
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
        public async Task<IActionResult> Create([Bind("UserName,SentDate,FormId,FormName,IP,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Feedback feedback)
        {
            feedback.FormId = feedback.Id;
            feedback.IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            feedback.CreatedBy = User.Identity.Name ?? "username";
            feedback.CreateDate = DateTime.Now;
            feedback.UpdatedBy = User.Identity.Name ?? "username";
            feedback.UpdateDate = DateTime.Now;
            feedback.AppTenantId = tenant.AppTenantId;
            feedback.SentDate = DateTime.Now;
            feedback.UserName = User.Identity.Name ?? "username";
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(feedback);
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
        public async Task<IActionResult> Edit(long id, [Bind("UserName,SentDate,FormId,FormName,IP,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Feedback feedback)
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
