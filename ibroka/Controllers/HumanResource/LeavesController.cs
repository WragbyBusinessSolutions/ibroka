﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E4S.Data;
using E4S.Models;
using E4S.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E4S.Controllers.HumanResource
{
    [Authorize]
    public class LeavesController : Controller
    {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    [TempData]
    public string StatusMessage { get; set; }

    public LeavesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;



    }
    private Guid getOrg()
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
      var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

      var orgdetails = _context.Organisations.Where(x => x.Id == orgId).FirstOrDefault();
      ViewData["OrganisationName"] = orgdetails.OrganisationName;
      ViewData["OrganisationImage"] = orgdetails.ImageUrl;

      return orgId;
    }

    public IActionResult Index()
        {
      var orgId = getOrg();

      var leaveList = _context.Leaves.Where(x => x.OrganisationId == orgId).Include(x => x.EmployeeDetail).OrderByDescending(x => x.DateCreated).ToList();

            ViewData["StatusMessage"] = StatusMessage;
            ViewData["StatusMessage"] = StatusMessage;
            return View(leaveList);
        }

    [HttpPost]
    public async Task<IActionResult> ApproveLeave([FromBody]PostApproveLeave postApprove)
    {
      if (postApprove == null)
      {
        return Json(new
        {
          msg = "No Data"
        }
       );
      }

      var orgId = getOrg();


      try
      {
        var leave = _context.Leaves.Where(x => x.Id == postApprove.Id).FirstOrDefault();
        leave.Comment = postApprove.Comment;
        leave.ApproveDate = DateTime.Now;
        leave.Status = "Approved";

        _context.Update(leave);
        _context.SaveChanges();

         StatusMessage = "Leave has been successfully Approved!.";

           return Json(new
        {
          msg = "Success"
        }
     );
      }
      catch (Exception ee)
      {

      }

      return Json(
      new
      {
        msg = "Fail"
      });
    }

    [HttpPost]
    public async Task<IActionResult> DeclineLeave([FromBody]PostApproveLeave postApprove)
    {
      if (postApprove == null)
      {
        return Json(new
        {
          msg = "No Data"
        }
       );
      }

      var orgId = getOrg();




      try
      {
        var leave = _context.Leaves.Where(x => x.Id == postApprove.Id).FirstOrDefault();
        leave.Comment = postApprove.Comment;
        leave.ApproveDate = DateTime.Now;
        leave.Status = "Declined";

        _context.Update(leave);
        _context.SaveChanges();

        StatusMessage = "Leave has been successfully Declined!.";

        return Json(new
        {
          msg = "Success"
        }
     );
      }
      catch (Exception ee)
      {

      }

      return Json(
      new
      {
        msg = "Fail"
      });
    }



  }
}