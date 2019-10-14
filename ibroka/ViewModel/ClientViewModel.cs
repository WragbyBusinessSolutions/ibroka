using ibroka.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class ClientViewModel
    {
        public List<Client> clients { get; set; }
        public int Age { get; set; }
        public int HotLeads { get; set; }
        public int WarmLeads { get; set; }
        public int CoolLeads { get; set; }
        public int ColdLeads { get; set; }
        public int AllLeads { get; set; }
        public int AllClients { get; set; }
        public int Last30Days { get; set; }
        public int Last7Days { get; set; }
        public int Yesterday { get; set; }
        public int Today { get; set; }



    }
}
