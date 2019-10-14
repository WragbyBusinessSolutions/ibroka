using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace E4S.Controllers.AccountInventory
{
    public class AllReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CommissionRebatesReport()
        {
            return View();
        }

        public IActionResult BiAnnualReport()
        {
            return View();
        }

        public IActionResult PersonnelReport()
        {
            return View();
        }

        public IActionResult RefundedPremiumReport()
        {
            return View();
        }

        public IActionResult ClientsBiAnnually()
        {
            return View();
        }
        public IActionResult MonthlyPremium()
        {
            return View();
        }
        public IActionResult BusinessSchedule()
        {
            return View();
        }

        public IActionResult ClientsAccount()
        {
            return View();
        }

        public IActionResult UnremittedPremium()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
    }
}