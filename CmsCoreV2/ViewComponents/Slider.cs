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
    public class Slider : ViewComponent
    {
        private readonly ApplicationDbContext context;
        

        public Slider(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {

            var slider = await GetSlider(name);
            if (slider == null)
            {
                slider = new CmsCoreV2.Models.Slider();
            }
            if (slider.Template != null)
            {
                return View(slider.Template, slider);
            }
            return View("Default", slider);

        }
        private async Task<Models.Slider> GetSlider(string sliderName)
        {
  

            return await GetSliderByName(sliderName, true);
        }
       
        public async Task<Models.Slider> GetSliderByName(string name, bool? status)
        {
            name = name.ToLower();
            var slider = await Get(s => s.Name == name, "Slides");
            if (status == null) { return slider; }
            if (status.HasValue && status.Value == true)
            {
                if (slider != null && slider.IsPublished)
                {
                    return slider;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return slider;
            }
        }
        public async Task<Models.Slider> Get(Expression<Func<Models.Slider, bool>> where, params string[] navigations)
        {
            var set = context.Sliders.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return await set.Where(where).FirstOrDefaultAsync();
        }


    }
}