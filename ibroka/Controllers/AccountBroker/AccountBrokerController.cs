using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ibroka.Controllers.AccountBroker
{
    public class AccountBrokerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Income()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            return View();
        }

        public IActionResult AccountReport()
        {
            return View();
        }

        public IActionResult Profit()
        {
            return View();
        }
        public IActionResult CashFlow()
        {
            return View();
        }
    public IActionResult Imprest()
    {
      return View();
    }
    }
}