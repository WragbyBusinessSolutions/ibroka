﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models
{
  public class BaseClass
  {
    public Guid OrganisationId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public DateTime DateModified { get; set; }
    public bool IsDeleted { get; set; }
    public BaseClass()
    {
      DateCreated = DateTime.Now;
      DateUpdated = DateTime.Now;
      DateModified = DateTime.Now;
    }

  }
}
