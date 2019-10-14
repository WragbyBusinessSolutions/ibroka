using ibroka.Models.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.ViewModel
{
  public class EmployeeQualificationViewModel
  {
    public List<InstitutionQualification> InstitutionQualifications { get; set; }
    public List<Skill> Skills { get; set; }
    public List<WorkExperience> WorkExperiences { get; set; }
  }
}
