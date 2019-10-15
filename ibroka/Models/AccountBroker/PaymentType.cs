using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.AccountBroker
{
  public class PaymentType : BaseClass
  {
    public Guid Id { get; set; }
    public string PaymentTypeName { get; set; }
    public string Description { get; set; }
  }
}
