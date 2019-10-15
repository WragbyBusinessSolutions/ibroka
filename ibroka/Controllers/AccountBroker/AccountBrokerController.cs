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

        // Edit Income Type

        [HttpPost]
        public async Task<IActionResult> EditIncomeType([FromBody]PostIncomeType postIncomeType)
        {
            if (postIncomeType == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }

            var orgId = getOrg();
            var organisationDetails = await _context.Organisations.Where(x => x.Id == orgId).FirstOrDefaultAsync();
           
            try
            {

                var IncType = _context.IncomeTypes.Where(x => x.Id == Guid.Parse(postIncomeType.AId)).FirstOrDefault();
                IncType.IncomeName = postIncomeType.IncomeName;
                IncType.Description = postIncomeType.Description;


                _context.Update(IncType);
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

        // Edit income Type Ended

        // Delete for Income Type

        private bool IncomeTypeExists(Guid id)
        {
            return _context.IncomeTypes.Any(e => e.Id == id);
        }


        [HttpPost]
        public IActionResult DeleteIncomeType([FromBody]string incomeTypeId)
        {
            if (incomeTypeId == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }

            try
            {
                var IncomeTitle = _context.IncomeTypes.SingleOrDefault(m => m.Id == Guid.Parse(incomeTypeId));
                _context.IncomeTypes.Remove(IncomeTitle);
                _context.SaveChanges();

                return Json(new
                {
                    msg = "Success"
                });

            }
            catch
            {

            }

            return Json(new
            {
                msg = "Fail"
            });


        }

        // End of income delete


        public IActionResult ExpenseType()
        {
            var expenseTpye = _context.ExpenseTypes.ToList();

            return View(expenseTpye);
        }


        // Post Method of income type
        [HttpPost]
        public async Task<IActionResult> PostExpenseType([FromBody]ExpenseType expenseType)
        {
            if (expenseType == null)
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
                ExpenseType newExpense = new ExpenseType()
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = expenseType.ExpenseName,
                    Description = expenseType.Description,
                    OrganisationId = orgId
                };

                _context.Add(newExpense);
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

        // Edit expense Type

        [HttpPost]
        public async Task<IActionResult> EditExpenseType([FromBody]PostExpenseType postExpenseType)
        {
            if (postExpenseType == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }

            var orgId = getOrg();
            var organisationDetails = await _context.Organisations.Where(x => x.Id == orgId).FirstOrDefaultAsync();

            try
            {

                var ExType = _context.ExpenseTypes.Where(x => x.Id == Guid.Parse(postExpenseType.AId)).FirstOrDefault();
                ExType.ExpenseName = postExpenseType.ExpenseName;
                ExType.Description = postExpenseType.Description;


                _context.Update(ExType);
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

        // Edit expense Type Ended

        // Delete for expense Type

        private bool ExpenseTypeExists(Guid id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }


        [HttpPost]
        public IActionResult DeleteExpenseType([FromBody]string expenseTypId)
        {
            if (expenseTypId == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }

            try
            {
                var ExpenseTitle = _context.ExpenseTypes.SingleOrDefault(m => m.Id == Guid.Parse(expenseTypId));
                _context.ExpenseTypes.Remove(ExpenseTitle);
                _context.SaveChanges();

                return Json(new
                {
                    msg = "Success"
                });

            }
            catch
            {

            }

            return Json(new
            {
                msg = "Fail"
            });


        }
    }
      public IActionResult PaymentType()
    {
      var orgId = getOrg();

      var paymentTypes = _context.PaymentTypes.Where(x => x.OrganisationId == orgId).ToList();

      return View(paymentTypes);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentType([FromBody]PaymentType paymentType)
    {
      if (paymentType == null)
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
        PaymentType newPaymentType = new PaymentType()
        {
          Id = Guid.NewGuid(),
          PaymentTypeName = paymentType.PaymentTypeName,
          Description = paymentType.Description,
          OrganisationId = orgId
        };

        _context.Add(newPaymentType);
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

  }
}