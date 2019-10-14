using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models
{
  public class Department : BaseClass
  {
    public Guid Id { get; set; }
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

  }
}
