using ibroka.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class EndorsementVM
    {
        public List<Endorsement> endorsements { get; set; }  

        public Policy policy { get; set; }
        public string policyClassId { get; set; }

        public string clientId { get; set; }

        public string viewType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Endorsement endorsement { get; set; }
    }
}
