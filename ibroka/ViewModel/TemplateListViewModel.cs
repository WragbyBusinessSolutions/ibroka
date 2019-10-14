using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class TemplateListViewModel
  {
    public AppraisalTemplate AppraisalTemplate { get; set; }
    public int NoOfCategory { get; set; }
    public float TotalWeight { get; set; }
  }
}
