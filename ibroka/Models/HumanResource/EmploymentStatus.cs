﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.HumanResource
{
  public class EmploymentStatus : BaseClass
  {
    public Guid Id { get; set; }
    public string EmploymentStatusName { get; set; }
    public string Description { get; set; }

  }
}
