using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.Models.LeadManagement
{
    public class LeadPolicyData
    {

        public Guid Id { get; set; }
        public Guid LeadPolicyFieldId { get; set; }
        public string Data { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public Guid LeadiId { get; set; }
    }
}
