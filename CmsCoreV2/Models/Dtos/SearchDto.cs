using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models.Dtos
{
    public class SearchDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public long ViewCount { get; set; }
    }
}
