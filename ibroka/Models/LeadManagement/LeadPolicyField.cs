using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class LeadPolicyField
    {
        public Guid Id { get; set; }
        public Guid PolicyClassId { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string FieldType { get; set; }
        public string FieldData { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
