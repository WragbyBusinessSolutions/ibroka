using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class AppraisalCategoryEdit
  {
    public AppraisalCategory AppraisalCategory { get; set; }
    public List<AppraisalKPI> AppraisalKPIs { get; set; }
  }
}
