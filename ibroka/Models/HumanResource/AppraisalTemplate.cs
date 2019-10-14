using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.HumanResource
{
  public class AppraisalTemplate : BaseClass
  {
    public Guid Id { get; set; }
    public string Template { get; set; }
  }
}
