using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.HumanResource
{
  public class AssignedSupervisor : BaseClass
  {
    public Guid Id { get; set; }
    public Guid EmployeeDetailId { get; set; }
    public virtual EmployeeDetail EmployeeDetail { get; set; }

    public Guid SupervisorId { get; set; }
    public string ReportMethod { get; set; }


  }
}
