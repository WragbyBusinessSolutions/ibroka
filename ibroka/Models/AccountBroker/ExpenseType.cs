﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.AccountBroker
{
  public class ExpenseType : BaseClass
  {
    public Guid Id { get; set; }
    public string ExpenseName { get; set; }
    public string Description { get; set; }

  }
}
