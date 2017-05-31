using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CmsCoreV2.ViewComponents
{
    public class EmbedForm : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public EmbedForm(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, string template)
        {
            var form = await GetForm(name, template);
            if (form == null)
            {
                form = new Form();
            }
            return View(template, form);

        }

        public virtual Form GetById(long id, params string[] navigations)
        {
            var set = _context.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.FirstOrDefault(f => f.Id == id);
        }

        public Form Get(Expression<Func<Form, bool>> where, params string[] navigations)
        {
            var set = _context.Forms.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.Where(where).FirstOrDefault<Form>();
        }

        public Form GetForm(long id)
        {
            var form = GetById(id, "FormFields");
            return form;
        }
        public Form GetForm(string name)
        {
            name = name.ToLower();
            var form = Get(f => f.FormName.ToLower() == name, "FormFields");
            return form;
        }

        private Task<CmsCoreV2.Models.Form> GetForm(string formName, string template)
        {
            return Task.FromResult(GetForm(formName));
        }
    }
}
