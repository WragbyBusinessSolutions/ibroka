using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class PayrollSummaryViewModel
  {
    public Guid Id { get; set; }
    public DateTime MonthYear { get; set; }
    public int NumberOfEmployee { get; set; }
    public float TotalMonthlySalary { get; set; }
    public float TotalPayables { get; set; }

  }
}
