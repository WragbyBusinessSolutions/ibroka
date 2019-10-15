using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.AccountBroker
{
  public class Income : BaseClass
  {
    public Guid Id { get; set; }
    public Guid IncomeTypeId { get; set; }

    public string Description { get; set; }
    public float Amount { get; set; }

  }
}
