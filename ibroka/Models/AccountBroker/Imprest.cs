using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.AccountBroker
{
  public class Imprest : BaseClass
  {
    public Guid Id { get; set; }
    public string Particular { get; set; }
    public float Amount { get; set; }
  }
}
