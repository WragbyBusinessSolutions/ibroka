﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.HumanResource
{
  public class JobTitle : BaseClass
  {
    public Guid Id { get; set; }
    public string JobTitleName { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }

  }
}
