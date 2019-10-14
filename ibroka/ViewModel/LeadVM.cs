using ibroka.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class LeadVM
    {
        public List<LeadPolicy> leads { get; set; }
        public int Age { get; set; }
        public int HotLeads { get; set; }
        public int WarmLeads { get; set; }
        public int CoolLeads { get; set; }
        public int ColdLeads { get; set; }
        public int AllLeads { get; set; }
    }
}
