using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class Lead
    {

        public Client Individual { get; set; }
        public Client Corporate { get; set; }
        public CorporateDirector[] Directors { get; set; }
        public CorporateDirector Director1 { get; set; }
        public CorporateDirector Director2 { get; set; }
        
        public LeadPolicy policy { get; set; }
        public string policyClassId { get; set; }
        
        public string clientId { get; set; }

        public List<LeadPolicy> leadPolicies { get; set; }
        public string viewType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public List<Policy> Policies { get; set; }
        public List<LeadEndorsement> LeadEndorsements { get; set; }
        [NotMapped]
        public int documentCount { get; set; }
    }
}
