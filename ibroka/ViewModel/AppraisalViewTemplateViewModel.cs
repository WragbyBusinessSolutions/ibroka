using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class AppraisalViewTemplateViewModel
  {
    public Guid Id { get; set; }
    public string TemplateName { get; set; }
    public List<AppCat> AppCat { get; set; }
  }

  public class AppCat
  {
    public AppraisalCategoryEdit AppraisalCategoryEdit { get; set; }
    public AppraisalTemplateCategory AppraisalTemplateCategory { get; set; }
  }

}
