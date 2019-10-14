using System;
using System.IO;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ibroka.Data;
using ibroka.Helpers;
using ibroka.Models;
using ibroka.Models.LeadManagement;
using ibroka.Services;
using ibroka.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ibroka.Controllers.LeadManagement
{
    public class LeadDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadDocumentController(ApplicationDbContext context)
        {
            _context = context;

        }

        
        public async  Task<ActionResult> AddLeadDocuments(Guid? lid)
        {
            LeadDocumentVM leadDocumentVM = new LeadDocumentVM();
            if (lid!=null && lid!=Guid.Empty)
            {
                leadDocumentVM = new LeadDocumentVM();
                leadDocumentVM.documents  = await _context.LeadDocuments.Where(a => a.LeadId == lid && a.IsDeleted==false).OrderBy(a=>a.DateCreated).ToListAsync();
                return PartialView("~/Views/Client/LeadDocumentPartial.cshtml", leadDocumentVM);
            }
            return PartialView("~/Views/Client/LeadDocumentPartial.cshtml", leadDocumentVM);
        }
    }
}