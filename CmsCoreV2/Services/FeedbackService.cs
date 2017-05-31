using CmsCoreV2.Data;
using CmsCoreV2.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CmsCoreV2.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Form> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    
        void FeedbackPost(IFormCollection collection, string ip, string appTenantId);
        void FeedbackPostMail(string body, long id);
        Form GetForm(long id);
        Form GetForm(string name);
  
        void DeleteForm(long id);
        long FormCount();
        List<FormField> GetFormFieldsByFormId(long id);
        void SaveForm();
    }

    public class FeedbackService:IFeedbackService
    {
        private readonly DbSet<Feedback> dbSet;
        private ApplicationDbContext DbContext;
        protected readonly AppTenant tenant;
        public FeedbackService(ApplicationDbContext context, ITenant<AppTenant> tenant)
        {
            DbContext = context;
            this.tenant = tenant?.Value;
        }

        public void FeedbackPost(IFormCollection collection, string ip, string appTenantId)
        {
            Feedback feed_back = new Feedback();
            feed_back.IP = ip;
            feed_back.AppTenantId = appTenantId;
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
                feedBackValue.AppTenantId = appTenantId;

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
         

            CreateFeedback(feed_back);
            SaveFeedback();
            body = body + "<br>" + "Gönderilme Tarihi : " + DateTime.Now;
            FeedbackPostMail(body, form.Id);
            //return feed_back.FeedbackValues.ToList();

        }

        public void CreateFeedback(Feedback Feedback)
        {
            DbContext.Add(Feedback);
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
                var setting = DbContext.Settings.FirstOrDefault();
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

        public IEnumerable<Form> FormSearch(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');


            var query = this.DbContext.Forms.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {

                    query = query.Where(c => c.FormName.Contains(sSearch) || c.Id.ToString().Contains(sSearch));
                }
            }
            var allForms = query;

            IEnumerable<Form> filteredForms = allForms;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredForms = filteredForms.OrderBy(c => c.Id);
                        break;
                    case 2:
                        filteredForms = filteredForms.OrderBy(c => c.FormName);
                        break;
                    default:
                        filteredForms = filteredForms.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {

                    case 1:
                        filteredForms = filteredForms.OrderByDescending(c => c.Id);
                        break;
                    case 2:
                        filteredForms = filteredForms.OrderByDescending(c => c.FormName);
                        break;
                    default:
                        filteredForms = filteredForms.OrderByDescending(c => c.Id);
                        break;
                }
            }

            var displayedForms = filteredForms.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedForms = displayedForms.Take(displayLength);
            }
            totalRecords = allForms.Count();
            totalDisplayRecords = filteredForms.Count();
            return displayedForms.ToList();
        }
        public IEnumerable<Form> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var forms = FormSearch(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);

            return forms;
        }
        public IEnumerable<Feedback> GetAll(params string[] navigations)
        {
            var set = DbContext.Feedbacks.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.AsEnumerable();
        }
        public IEnumerable<Feedback> GetForms()
        {
            var forms = GetAll(); // içerde FormField yazıyordu
            return forms;
        }
        public Feedback GetById(long id, params string[] navigations)
        {
            var set = DbContext.Feedbacks.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.FirstOrDefault(f => f.Id == id);
        }
        public Form GetFormById(long id, params string[] navigations)
        {
            var set = DbContext.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.FirstOrDefault(f => f.Id == id);
        }
        public Form GetForm(long id)
        {
            var form = GetFormById(id, "FormFields");
            return form;
        }
        public Feedback Get(Expression<Func<Feedback, bool>> where, params string[] navigations)
        {
            var set = DbContext.Feedbacks.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.Where(where).FirstOrDefault<Feedback>();
        }
        public Form GetForm(string name)
        {
            name = name.ToLower();
            var form = DbContext.Forms.AsQueryable().Include("FormFields").Where(f => f.FormName.ToLower() == name).FirstOrDefault();
            return form;
        }
        public List<FormField> GetFormFieldsByFormId(long id)
        {
            Form form = GetFormById(id, "FormFields");
            return form.FormFields.OrderBy(c => c.Position).ToList();
        }
        public virtual void Add(Feedback entity)
        {
            var date = DateTime.Now;
            entity.CreatedBy = "username";
            entity.CreateDate = date;
            entity.UpdatedBy = "username";
            entity.UpdateDate = date;
            entity.AppTenantId = tenant.AppTenantId;
            DbContext.Add(entity);
        }

        public virtual void Update(Feedback entity)
        {
            entity.UpdatedBy = "username";
            entity.UpdateDate = DateTime.Now;
            entity.AppTenantId = tenant.AppTenantId;
            dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(Feedback entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<Feedback, bool>> where)
        {
            IEnumerable<Feedback> objects = dbSet.Where<Feedback>(where).AsEnumerable();
            foreach (Feedback obj in objects)
                dbSet.Remove(obj);
        }
        public void CreateForm(Feedback feedback)
        {
            Add(feedback);
        }
        public void UpdateForm(Feedback feedback)
        {
            Update(feedback);
        }
        public void DeleteForm(long id)
        {
            Delete(f => f.Id == id);
        }
        

        public void Commit()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void SaveForm()
        {
            Commit();
        }
        public long FormCount()
        {
            return GetAll().Count();
        }
    }
}
