using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.ViewModel
{
  public class HomeDashboardViewModel
  {
    public int HumanCapacityReport { get; set; }
    public float TotalAmount { get; set; }
    public int Claim { get; set; }


    public int Leads { get; set; }
    public int Clients { get; set; }

    public List<HeadCount> HeadCounts { get; set; }

  }



}
