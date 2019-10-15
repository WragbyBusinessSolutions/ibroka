using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ibroka.Data;
using ibroka.Models;
using ibroka.Models.AccountBroker;
using ibroka.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ibroka.Controllers.AccountBroker
{
    public class AccountBrokerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountBrokerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
    //public IActionResult Imprest()
    //{
    //  return View();
    //}


        public IActionResult Imprest()
        {
            var orgId = getOrg();
            var Imprsts = _context.Imprests.Where(x => x.OrganisationId == orgId).ToList();

            return View(Imprsts);
        }

        [HttpPost]
        public async Task<IActionResult> postNewImprest([FromBody]PostNewImprest postNewImprest)
        {
            if (postNewImprest == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }

            var orgId = getOrg();
            var organisationDetails = await _context.Organisations.Where(x => x.Id == orgId).FirstOrDefaultAsync();
            int noOfEmployee = _context.Users.Where(x => x.OrganisationId == orgId).Count();

            try
            {
                Imprest newImprest = new Imprest()
                {
                    Id = Guid.NewGuid(),
                    Particular = postNewImprest.Particular,
                    Amount = postNewImprest.Amount,
                    OrganisationId = orgId
                };

                _context.Add(newImprest);
                _context.SaveChanges();


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

        public IActionResult IncomeType()
        {
            var incomeTpye = _context.IncomeTypes.ToList();

            return View(incomeTpye);
        }


        // Post Method of income type
        [HttpPost]
        public async Task<IActionResult> PostIncomeType([FromBody]IncomeType incomeType)
        {
            if (incomeType == null)
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
                IncomeType newIncome = new IncomeType()
                {
                    Id = Guid.NewGuid(),
                    IncomeName = incomeType.IncomeName,
                    Description = incomeType.Description,
                    OrganisationId = orgId
                };

                _context.Add(newIncome);
                _context.SaveChanges();


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


        public IActionResult ExpenseType()
        {
            return View();
        }

    }
}