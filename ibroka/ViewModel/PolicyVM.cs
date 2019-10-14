using E4S.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.ViewModel
{
    public class PolicyVM
    {
        public List<Policy> policies { get; set; }
        public int Age { get; set; }
        public int HotLeads { get; set; }
        public int WarmLeads { get; set; }
        public int CoolLeads { get; set; }
        public int ColdLeads { get; set; }
        public int AllLeads { get; set; }
    }
}
