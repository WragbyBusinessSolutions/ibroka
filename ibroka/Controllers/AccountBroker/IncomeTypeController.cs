using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ibroka.Controllers.AccountBroker
{
    public class IncomeTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}