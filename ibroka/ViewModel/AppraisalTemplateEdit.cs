using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class AppraisalTemplateEdit
  {
    public AppraisalTemplate AppraisalTemplate { get; set; }
    public List<AppraisalTemplateCategory> Categories { get; set; }
    public int Weight { get; set; }
    

  }
}
