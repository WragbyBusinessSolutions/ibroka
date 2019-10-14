using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E4S.Data;
using E4S.Models.LeadManagement;
using Microsoft.AspNetCore.Mvc;

namespace E4S.Controllers.WragbyAdmin
{
    public class AdminController : Controller
    {
    private readonly ApplicationDbContext _context;


    public AdminController(ApplicationDbContext context)
    {
      _context = context;

    }

    public IActionResult Index()
        {
            return View();
        }


        public IActionResult CompanyListing()
        {
            return View();
        }

        public IActionResult UpdateOrganizationDetails()
        {
            return View();
        }

    public IActionResult PolicyClassConfiguration()
    {
      var allPolicy = _context.PolicyClassMaster.ToList();

      return View(allPolicy);
    }

    public IActionResult AddPolicyClass()
    {

      return View();
    }

    [HttpPost]
    public IActionResult AddPolicyClass(PolicyClassMaster policyClassMaster)
    {

      if (policyClassMaster == null)
      {
        return View();
      }

      policyClassMaster.Id = Guid.NewGuid();

      try
      {
        _context.Add(policyClassMaster);
        _context.SaveChanges();

        return RedirectToAction("PolicyClassConfiguration");
      }
      catch
      {

      }


      return View();
    }

    public IActionResult EditPolicyClass(Guid id)
    {
      var policy = _context.PolicyClassMaster.FirstOrDefault(x => x.Id == id);
      return View(policy);
    }

    [HttpPost]
    public IActionResult EditPolicyClass(PolicyClassMaster policyClassMaster)
    {

      if (policyClassMaster == null)
      {
        return View();
      }

       var policy = _context.PolicyClassMaster.FirstOrDefault(x => x.Id == policyClassMaster.Id);
      policy.Name = policyClassMaster.Name;
      policy.CommisionPercent = policyClassMaster.CommisionPercent;

      try
      {
        _context.PolicyClassMaster.Update(policy);
        _context.SaveChanges();

        return RedirectToAction("PolicyClassConfiguration");
      }
      catch
      {

      }


      return View();
    }



  }
}