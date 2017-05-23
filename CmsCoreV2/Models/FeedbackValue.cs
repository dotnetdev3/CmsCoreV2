using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Models
{
    public class FeedbackValue : BaseEntity
    {
        public int? FormFieldId { get; set; }
        public string FormFieldName { get; set; }
        public FieldType FieldType { get; set; }
        public int Position { get; set; }
        public string Value { get; set; }
        public long FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}
