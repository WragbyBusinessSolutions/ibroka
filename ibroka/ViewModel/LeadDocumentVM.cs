using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E4S.Models.LeadManagement;
namespace E4S.ViewModel
{
    public class LeadDocumentVM
    {

     public   List<LeadDocument> documents { get; set; }

    }
}
