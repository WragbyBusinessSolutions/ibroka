using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.HumanResource
{
  public class JobCategory : BaseClass
  {
    public Guid Id { get; set; }
    public string JobCategoryName { get; set; }
    public string Description { get; set; }

  }
}
