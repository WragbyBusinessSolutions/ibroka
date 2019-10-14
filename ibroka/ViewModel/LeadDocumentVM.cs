using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ibroka.Models.LeadManagement;
namespace ibroka.ViewModel
{
    public class LeadDocumentVM
    {

     public   List<LeadDocument> documents { get; set; }

    }
}
