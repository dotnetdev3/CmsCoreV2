using CmsCoreV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.Entities
{
    public class Slider : BaseEntity
    {
        public Slider()
        {
            IsPublished = true;
            Slides = new HashSet<Slide>();
        }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<Slide> Slides { get; set; }
        [StringLength(200)]
        public string Template { get; set; }
    }
}