using ibroka.Models;
using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class ReportViewModel
  {
    public EmployeeDetail EmployeeDetails { get; set; }
    public Job Job { get; set; }
    public Salary Salary { get; set; }
    public ContactDetail ContactDetail { get; set; }


  }
}
