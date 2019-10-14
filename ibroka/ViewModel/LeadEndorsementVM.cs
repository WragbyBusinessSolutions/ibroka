using E4S.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.ViewModel
{
    public class LeadEndorsementVM
    {
        public List<LeadEndorsement> leadEndorsements { get; set; }

        public Policy policy { get; set; }
        public string policyClassId { get; set; }

        public string clientId { get; set; }

        public string viewType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public LeadEndorsement leadEndorsement { get; set; }
        public List<Agent> AgentList { get; set; }
    }
}
