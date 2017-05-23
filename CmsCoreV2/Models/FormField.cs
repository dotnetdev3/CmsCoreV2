using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class FormField:BaseEntity
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Value { get; set; }
        public int Position { get; set; }

        public FieldType FieldType { get; set; }

        public long? FormId { get; set; }
        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }
    }
}
