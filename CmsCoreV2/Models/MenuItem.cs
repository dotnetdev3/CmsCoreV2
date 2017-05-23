﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int Position { get; set; }
        public bool IsPublished { get; set; }
        public long? ParentMenuItemId { get; set; }
        public virtual MenuItem ParentMenuItem { get; set; }
        public ICollection<MenuItem> ChildMenuItems { get; set; }
        public long? MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
