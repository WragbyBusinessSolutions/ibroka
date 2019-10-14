using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ibroka.Controllers.AccountInventory
{
    public class OtherexpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}