using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
    public class PostNewAssignSupervisors
    {
        public string AId { get; set; }
        public Guid EmployeeDetailsId { get; set; }
        public Guid SupervisorId { get; set; }
        public string ReportMethod  { get; set; }
        public string AutoList { get; set; }
    }
}
