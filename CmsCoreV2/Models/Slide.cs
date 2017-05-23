﻿using CmsCoreV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.Entities
{
    public class Slide : BaseEntity
    {
        public Slide()
        {
            DisplayTexts = false;
            IsPublished = true;
        }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public string Photo { get; set; }
        public string Video { get; set; }
        public string CallToActionText { get; set; }
        public string CallToActionUrl { get; set; }
        public bool DisplayTexts { get; set; }
        public bool IsPublished { get; set; }
        public long SliderId { get; set; }
        public virtual Slider Slider { get; set; }

    }
}