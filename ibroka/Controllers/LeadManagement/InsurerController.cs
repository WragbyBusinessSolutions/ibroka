using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E4S.Data;
using E4S.Helpers;
using E4S.Models.LeadManagement;
using E4S.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E4S.Controllers.LeadManagement
{
    public class InsurerController : Controller
    {

        private readonly ApplicationDbContext _context;

        public InsurerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["StatusMessage"] = StatusMessage;
            return View();
        }


        public async Task<IActionResult> GetInsurers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var insurers = await _context.InsurerMasters.Where(i => i.OrganisationId == orgId).ToListAsync();


            return Json(insurers);
            //return clients;
        }

        public IActionResult AddInsurer()
        {
            InsurerVM insurerVM = new InsurerVM();
            insurerVM.insurerMaster = new InsurerMaster();
            insurerVM.insurerMaster.NewRecord = "new";

            return PartialView("InsurerAddPartialView", insurerVM);
        }

        //

        public async Task<IActionResult> SaveInsurer(InsurerVM insurerVM)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            InsurerMaster insurerMaster = new InsurerMaster();

            insurerMaster.Id = Guid.NewGuid();
            insurerMaster.Address = insurerVM.insurerMaster.Address;
            insurerMaster.Commission = insurerVM.insurerMaster.Commission;
            insurerMaster.DateCreated = DateTime.Now;
            insurerMaster.DateModified = DateTime.Now;
            insurerMaster.DateUpdated = DateTime.Now;
            insurerMaster.Description = insurerVM.insurerMaster.Description;
            insurerMaster.DisplayName = insurerVM.insurerMaster.DisplayName;
            insurerMaster.Email = insurerVM.insurerMaster.Email;
            insurerMaster.GlobalInsurerId = Guid.Empty;
            insurerMaster.Name = insurerVM.insurerMaster.Name;
            insurerMaster.IsDeleted = false;
            insurerMaster.OrganisationId = orgId;
            insurerMaster.PhoneNo = insurerVM.insurerMaster.PhoneNo;
            insurerMaster.WebUrl = insurerVM.insurerMaster.WebUrl;

            _context.Add(insurerMaster);

            await _context.SaveChangesAsync();

            StatusMessage = StaticContent.INSURER_CREAT_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetInsurerById(Guid? id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            InsurerMaster insurerMaster = await _context.InsurerMasters.Where(i => i.Id == id).FirstOrDefaultAsync();
            insurerMaster.NewRecord = "edit";

            return PartialView("InsurerPartialView", insurerMaster);
        }

        public async Task<IActionResult> EditInsurer(InsurerMaster insMaster  )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            InsurerMaster insurerMaster = await _context.InsurerMasters.Where(i => i.Id == insMaster.Id).FirstOrDefaultAsync();

            insurerMaster.Address = insMaster.Address;
            insurerMaster.Commission = insMaster.Commission;
            insurerMaster.Description = insMaster.Description;
            insurerMaster.DisplayName = insMaster.DisplayName;
            insurerMaster.Email = insMaster.Email;
            insurerMaster.Name = insMaster.Name;
            insurerMaster.PhoneNo = insMaster.PhoneNo;
            insurerMaster.WebUrl = insMaster.WebUrl;

            _context.Update(insurerMaster);
            await _context.SaveChangesAsync();

            StatusMessage = StaticContent.INSURER_Edit_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;

            return PartialView("Index");
        }

        [HttpPost]
        public JsonResult CheckInsurerExist([FromBody]InsurerMaster im)
        {
            
            if (im.NewRecord == "new")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
                im.OrganisationId = orgId;
            }

            InsurerMaster data = _context.InsurerMasters.Where(i => i.OrganisationId == im.OrganisationId && i.Name == im.Name.Trim()).FirstOrDefault();
            if (data == null)
            {
                return Json("notexist");
            }
            else
            {
                if (im.Id == Guid.Empty)
                {
                    return Json("exist");
                }
                else
                {
                    if (data.Id == im.Id)
                    {
                        return Json("notexist");
                    }
                    else
                    {
                        return Json("exist");
                    }
                }
            }

            return Json("exist");
        }
    }
}
