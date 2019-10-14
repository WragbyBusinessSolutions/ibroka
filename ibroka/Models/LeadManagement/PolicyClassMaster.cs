using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class PolicyClassMaster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    
        public float CommisionPercent { get; set; }
    }
}
