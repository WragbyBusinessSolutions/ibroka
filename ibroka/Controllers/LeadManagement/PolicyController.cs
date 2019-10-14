using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ibroka.Data;
using ibroka.Helpers;
using ibroka.Models;
using ibroka.Models.LeadManagement;
using ibroka.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860 

namespace ibroka.Controllers.LeadManagement
{
    public class PolicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            //var gInsurer = await _context.GlobalInsurers.Where(g => g.Id == orgId).ToListAsync();
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var policyVM = new PolicyVM();


            var clients = await _context.Clients.Where(a => a.OrganisationId == orgId).ToListAsync();
            policyVM.policies = await _context.Policies.Where(p => p.OrganisationId == orgId).ToListAsync();

            //process clients to get the different types of leads
            policyVM.HotLeads = 0;
            policyVM.WarmLeads = 0;
            policyVM.CoolLeads = 0;
            policyVM.ColdLeads = 0;
            policyVM.AllLeads = 0;
            policyVM.Age = 0;
            int hotLeadDays = 0;
            int warmLeadDays = 0;
            int coolLeadDays = 0;
            int coldLeadDays = 0;

            var configOptions = await _context.BussinessOperationConfigurations.Where(b => b.OrganisationId == orgId).ToListAsync();
            //var configOptions = await _context.BussinessOperationConfigurations.ToListAsync();

            hotLeadDays = int.Parse(configOptions.Where(c => c.Key == "HotLeads").FirstOrDefault().Value);
            warmLeadDays = int.Parse(configOptions.Where(c => c.Key == "WarmLeads").FirstOrDefault().Value);
            coolLeadDays = int.Parse(configOptions.Where(c => c.Key == "CoolLeads").FirstOrDefault().Value);
            coldLeadDays = int.Parse(configOptions.Where(c => c.Key == "ColdLeads").FirstOrDefault().Value);

            foreach (var client in clients)
            {
                client.Age = 0;

                client.Age = Util.CalculateAge(client.DateOfBirth);


            }

            foreach (var policy in policyVM.policies)
            {
                //client.Age = 0;

                int days = Util.GetDaysBetweenCurretAndGivenDate(policy.DateCreated);

                if (days <= hotLeadDays)
                {
                    policyVM.HotLeads++;
                }
                else if (days > hotLeadDays && days <= warmLeadDays)
                {
                    policyVM.WarmLeads++;
                }
                else if (days > warmLeadDays && days <= coolLeadDays)
                {
                    policyVM.CoolLeads++;
                }
                else
                {
                    policyVM.ColdLeads++;
                }

                policyVM.AllLeads++;

                var clientInfo = clients.Where(c => c.Id == policy.ClientId).FirstOrDefault();

                policy.Name = clientInfo.Name;
                policy.Age = clientInfo.Age;
                policy.PhoneNo = clientInfo.PhoneNo;
                policy.Email = clientInfo.Email;
                policy.Type = clientInfo.Type;
                
                policy.InsurerName = _context.InsurerMasters.Where(i => i.Id == policy.InsurerMasterId).FirstOrDefault().Name;

                policy.PolicyName = Util.GetPolicyName(policyClasses, policy.PolicyClassId);


            }
            var sortedList = policyVM.policies.OrderBy(l => l.DateCreated).Reverse().ToList();

            policyVM.policies = sortedList;
            return View(policyVM);
        }

        public async Task<IActionResult> PolicyDeatils(string llid)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();
            string lid = llid;

            Lead leadData = new Lead();
            leadData.viewType = "viewdetails";

            leadData.policy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(lid)).FirstOrDefault();
            leadData.startDate = leadData.policy.StartDate.ToString("yyyy-MM-dd");
            leadData.endDate = leadData.policy.EndDate.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(leadData.policy.PolicyNo))
            {
                leadData.policy.PolicyNo = "TBA";
            }


            var lpData = new List<dynamic>();


            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == leadData.policy.PolicyClassId).ToListAsync();
            List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == leadData.policy.Id).ToListAsync();

            leadData.policy.AgentList = Util.getAgentListFromJson(leadData.policy.Agents);

            string policyName = "";
            leadData.policyClassId = leadData.policy.PolicyClassId.ToString();

            foreach (LeadPolicyField lpf in leadPolicyFields)
            {
                dynamic DyObj = new ExpandoObject();
                foreach (LeadPolicyData lpd in leadsData)
                {
                    if (lpd.LeadPolicyFieldId == lpf.Id)
                    {
                        DyObj.FieldName = lpf.FieldName;
                        DyObj.DisplayName = lpf.DisplayName;
                        DyObj.FieldType = lpf.FieldType;
                        DyObj.FieldValue = lpd.Data;
                        DyObj.FieldName = lpf.FieldName;
                        DyObj.PolicyName = policyName;

                        lpData.Add(DyObj);
                    }
                }
            }


            ViewBag.LeadsData = lpData;



            return PartialView("PolicyViewPartial", leadData);

        }

        void getPolicyMasterData()
        {
            var olist = _context.PolicyClassMaster.ToList();
            List<SelectListItem> ObjList = new List<SelectListItem>();

            foreach (PolicyClassMaster pcm in olist)
            {
                SelectListItem si = new SelectListItem { Text = pcm.Name, Value = pcm.Id.ToString() };
                ObjList.Add(si);
            }


            ViewBag.PolicyClasses = ObjList;
        }

        void getInsurerMasterData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            //var globalList = _context.GlobalInsurers.ToList();
            var insMasterLst = _context.InsurerMasters.Where(i => i.OrganisationId == orgId).ToList();

            List<SelectListItem> ObjList = new List<SelectListItem>();

            foreach (InsurerMaster im in insMasterLst)
            {
                var name = im.Name; //globalList.Where(g => g.Id == im.GlobalInsurerId).FirstOrDefault().Name;

                SelectListItem si = new SelectListItem { Text = name, Value = im.Id.ToString() };
                ObjList.Add(si);
            }

            var sortedList = ObjList.OrderBy(o => o.Text).ToList();

            ViewBag.InsuresList = sortedList;
        }

        void getCurrencyList()
        {

            var currency_List = _context.Currency.ToList();

            List<SelectListItem> ObjList = new List<SelectListItem>();

            foreach (Currency c in currency_List)
            {

                SelectListItem si = new SelectListItem { Text = c.Code, Value = c.Code };
                ObjList.Add(si);
            }

            ViewBag.currencyList = ObjList;
        }

        public async Task<ActionResult> GetPolicyCreditNoteData(Guid? lid)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var pcVM = new PolicyCreditNoteVM();

            Policy policy = _context.Policies.Where(p => p.LeadId == lid).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == policy.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var leadPaymentDetail = _context.LeadPaymentDetails.Where(d => d.DebitNoteId == policy.DebitNoteId).FirstOrDefault();
            var insurerId = _context.InsurerMasters.Where(i => i.Id == policy.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            var policyMasters = _context.PolicyClassMaster.ToList();

            pcVM.policy = policy;
            pcVM.addresss = organisation.Address;
            pcVM.organisationName = organisation.OrganisationName;
            pcVM.amountReceived = "";
            pcVM.city = organisation.City;
            pcVM.currency = "";
            pcVM.customerAddress = client.Address;
            pcVM.email = organisation.Email;
            pcVM.endorsementNo = "";

            pcVM.paymentMode = "";
            pcVM.phone = organisation.PhoneNumber;
            pcVM.policyName = Util.GetPolicyName(policyMasters, policy.PolicyClassId);
            if (string.IsNullOrEmpty(pcVM.policy.PolicyNo))
            {
                pcVM.policyNo = "TBA";
            }
            else
            {
                pcVM.policyNo = pcVM.policy.PolicyNo;
            }
            pcVM.riskType = "";
            pcVM.transcationType = "";
            pcVM.branchName = branch.BranchName;
            pcVM.customerNo = client.ClientCode.ToString();
            pcVM.insurerName = Util.GetInsurerName(_context, insurerId);
            pcVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            pcVM.Client = client;

            var policyCreditNote = new PolicyCreditNote();

            if (policy.CreditNoteId == null || policy.CreditNoteId == Guid.Empty)
            {
                policyCreditNote.PolicyNo = policy.PolicyNo;
                policyCreditNote.EndorsementNo = policy.EndorsementNo;
                policyCreditNote.Id = Guid.NewGuid();
                policyCreditNote.CreatedBy = Guid.Parse(userId);
                policyCreditNote.ModifiedBy = Guid.Parse(userId);
                policyCreditNote.PolicyId = policy.Id;
                policyCreditNote.OrganisationId = orgId;
                _context.Add(policyCreditNote);

                policy.CreditNoteId = policyCreditNote.Id;

                _context.Update(policy);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            pcVM.policyCreditNote = _context.PolicyCreditNotes.Where(c => c.Id == policy.CreditNoteId).OrderByDescending(a => a.DateCreated).SingleOrDefault();
            pcVM.preparedBy = _context.Users.Where(x => x.Id == pcVM.policyCreditNote.CreatedBy.ToString()).FirstOrDefault().FirstName;


            return PartialView("PolicyCreditNotePartial", pcVM);
        }


        public async Task<IActionResult> SavePolicyNo([FromBody]Policy policy)
        {
            if (policy == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var selectedPolicy = _context.Policies.Where(p => p.Id == policy.Id).FirstOrDefault();

                selectedPolicy.DateUpdated = DateTime.Now;
                selectedPolicy.ModifiedBy = Guid.Parse(userId);
                selectedPolicy.DateModified = DateTime.Now;
                selectedPolicy.PolicyNo = policy.PolicyNo;
                //selectedPolicy.EndorsementNo = policy.EndorsementNo;

                _context.Update(selectedPolicy);

                var leadPolicy = _context.LeadPolicies.Where(l => l.Id == selectedPolicy.LeadId).FirstOrDefault();
                leadPolicy.PolicyNo = selectedPolicy.PolicyNo;
                //leadPolicy.EndorsementNo = selectedPolicy.EndorsementNo;

                _context.Update(leadPolicy);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string aa = ex.Message;
                    string bb = "";
                }

                StatusMessage = StaticContent.POLICYNO_UPDATE;
                ViewData["StatusMessage"] = StatusMessage;

                return Json(new
                {
                    msg = "Success"
                });
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }

            return Json(new
            {
                msg = "Fail"
            });
        }

        public async Task<IActionResult> UploadReceipt(IFormFile file, IFormCollection formCollection)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //return Content("file not selected");

            var filename = Guid.NewGuid().ToString() + "_" + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Policy", "Receipts", filename);
            var path2 = Path.Combine("Policy", "Receipts", filename);

            var policyId = formCollection["puPolicyId"].ToString();
            var policy = _context.Policies.Where(x => x.Id == Guid.Parse(policyId)).FirstOrDefault();
            policy.ReceiptNo = formCollection["txtReceiptNo"].ToString();
            policy.ReceiptDate = Convert.ToDateTime(formCollection["txtReceiptDate"].ToString());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                policy.ReceiptImgUrl = path2;

                _context.Update(policy);
                _context.SaveChanges();

            }

            StatusMessage = StaticContent.RECEIPT_UPLOAD_SUCCESS_MSG;
            ViewData["StatusMessage"] = StatusMessage;

            //return RedirectToAction("Index");
            var routeValues = new RouteValueDictionary {
  { "id", policy.ClientId },
   { "IsOpen", "policies" }
};
            return RedirectToAction("Details", "Client", routeValues);
        }


        public async Task<IActionResult> GetEndorsementById(string id)
        {
            LeadEndorsementVM leadEndorsementVM = new LeadEndorsementVM();
            leadEndorsementVM = await GetEndorsementData("", id);
            return PartialView("EndorsementPartialView", leadEndorsementVM);

        }

        public async Task<IActionResult> createEndorsement(string pid)
        {
            LeadEndorsementVM leadEndorsementVM = new LeadEndorsementVM();

            leadEndorsementVM = await GetEndorsementData(pid, "");
            return PartialView("AddLeadEndorsementView", leadEndorsementVM);

        }

        private async Task<LeadEndorsementVM> GetEndorsementData(string policyId, string leid)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();

            LeadEndorsementVM leadEndorsementVM = new LeadEndorsementVM();

            if (string.IsNullOrEmpty(leid))
            {
                leadEndorsementVM.leadEndorsement = new LeadEndorsement();
            }
            else
            {
                leadEndorsementVM.leadEndorsement = _context.LeadEndorsements.Where(l => l.Id == Guid.Parse(leid)).FirstOrDefault();

                leadEndorsementVM.startDate = leadEndorsementVM.leadEndorsement.StartDate.ToString("yyyy-MM-dd");
                leadEndorsementVM.endDate = leadEndorsementVM.leadEndorsement.EndDate.ToString("yyyy-MM-dd");
                leadEndorsementVM.AgentList = Util.getAgentListFromJson(leadEndorsementVM.leadEndorsement.Agents);
            }

            if (!string.IsNullOrEmpty(policyId))
                leadEndorsementVM.policy = _context.Policies.Where(p => p.Id == Guid.Parse(policyId)).FirstOrDefault();
            else
                leadEndorsementVM.policy = _context.Policies.Where(p => p.Id == leadEndorsementVM.leadEndorsement.PolicyId).FirstOrDefault();

            var lpData = new List<dynamic>();
            var policyMaster = await _context.PolicyClassMaster.ToListAsync();

            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == leadEndorsementVM.policy.PolicyClassId).ToListAsync();
            List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == leadEndorsementVM.policy.LeadId).ToListAsync();

            leadEndorsementVM.policy.AgentList = Util.getAgentListFromJson(leadEndorsementVM.policy.Agents);
            leadEndorsementVM.policy.PolicyName = Util.GetPolicyName(policyMaster, leadEndorsementVM.policy.PolicyClassId);
            var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == leadEndorsementVM.policy.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            leadEndorsementVM.policy.InsurerName = Util.GetInsurerName(_context, globalInsurerId);


            leadEndorsementVM.policy.AgentList = Util.getAgentListFromJson(leadEndorsementVM.policy.Agents);

            foreach (LeadPolicyField lpf in leadPolicyFields)
            {
                dynamic DyObj = new ExpandoObject();
                foreach (LeadPolicyData lpd in leadsData)
                {
                    if (lpd.LeadPolicyFieldId == lpf.Id)
                    {
                        DyObj.FieldName = lpf.FieldName;
                        DyObj.DisplayName = lpf.DisplayName;
                        DyObj.FieldType = lpf.FieldType;
                        DyObj.FieldValue = lpd.Data;
                        DyObj.FieldName = lpf.FieldName;

                        lpData.Add(DyObj);
                    }
                }
            }


            ViewBag.LeadsData = lpData;

            return leadEndorsementVM;
        }

        public async Task<IActionResult> AddEndorsement(LeadEndorsementVM leadEndorsementVM, IFormCollection formCollection)
        {
            Guid ClientId = Guid.Parse(formCollection["policy.ClientId"].ToString());
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pid = formCollection["policy.Id"].ToString();
            var policy = _context.Policies.Where(p => p.Id == Guid.Parse(pid)).FirstOrDefault();
            var lpolicy = _context.LeadPolicies.Where(l => l.Id == policy.LeadId).FirstOrDefault();

            LeadEndorsement leadEndorsement = new LeadEndorsement();

            leadEndorsement.Id = Guid.NewGuid();
            leadEndorsement.ClientId = ClientId;
            leadEndorsement.Commission = leadEndorsementVM.leadEndorsement.Commission;
            leadEndorsement.Description = leadEndorsementVM.leadEndorsement.Description;

            leadEndorsement.CreatedBy = Guid.Parse(userId);
            leadEndorsement.DateCreated = DateTime.Now;
            leadEndorsement.DateModified = DateTime.Now;
            leadEndorsement.DateUpdated = DateTime.Now;
            leadEndorsement.EndDate = Convert.ToDateTime(formCollection["EndDate"].ToString());
            leadEndorsement.GrossPremium = leadEndorsementVM.leadEndorsement.GrossPremium;
            leadEndorsement.IsDeleted = false;

            leadEndorsement.ModifiedBy = Guid.Parse(userId);
            leadEndorsement.NetPremium = leadEndorsementVM.leadEndorsement.NetPremium;
            leadEndorsement.OrganisationId = Guid.Parse(formCollection["policy.OrganisationId"].ToString());
            leadEndorsement.PolicyClassId = policy.PolicyClassId;

            leadEndorsement.StartDate = Convert.ToDateTime(formCollection["StartDate"].ToString());
            leadEndorsement.Status = "New";

            leadEndorsement.InsurerMasterId = policy.InsurerMasterId;
            leadEndorsement.Currency = policy.Currency;
            leadEndorsement.TranscationType = leadEndorsementVM.leadEndorsement.TranscationType;
            ///get Agents           
            leadEndorsement.Agents = Util.getAgents(formCollection);
            leadEndorsement.LeadId = policy.LeadId;
            leadEndorsement.PolicyId = policy.Id;


            _context.Add(leadEndorsement);

            policy.LeadEndorsementId = leadEndorsement.Id;
            lpolicy.LeadEndorsementId = leadEndorsement.Id;

            int cnt = Convert.ToInt32( policy.EndorsementCount);
            cnt++;
            policy.EndorsementCount = cnt;

            _context.Update(policy);
            _context.Update(lpolicy);
            await _context.SaveChangesAsync();


            StatusMessage = StaticContent.LEAD_ENDORSEMENT_CREATE_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;

            var routeValues = new RouteValueDictionary {
  { "id", ClientId },
  { "IsOpen", "leads" }

};
            return RedirectToAction("Details", "Client", routeValues);

        }

        public async Task<IActionResult> UpdateEndorsement(LeadEndorsementVM leadEndorsementVM, IFormCollection formCollection)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            LeadEndorsement leadEndorsement = new LeadEndorsement();
            leadEndorsement = leadEndorsementVM.leadEndorsement;

            leadEndorsement.EndDate = Convert.ToDateTime(formCollection["EndDate"].ToString());

            leadEndorsement.IsDeleted = false;
            leadEndorsement.ModifiedBy = Guid.Parse(userId);
            leadEndorsement.DateModified = DateTime.Now;
            leadEndorsement.StartDate = Convert.ToDateTime(formCollection["StartDate"].ToString());

            leadEndorsement.Agents = Util.getAgents(formCollection);

            _context.Update(leadEndorsement);
            await _context.SaveChangesAsync();

            StatusMessage = StaticContent.LEAD_ENDORSEMENT_UPDATE_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;

            var routeValues = new RouteValueDictionary {
  { "id", leadEndorsement.ClientId },
  { "IsOpen", "policies" }

};
            return RedirectToAction("Details", "Client", routeValues);

        }

        public async Task<ActionResult> GenerateEndorseDebitNote(Guid? leid)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var endorsementDebitVM = new EndorsementDebitNoteVM();

            LeadEndorsement leadEndorsement = _context.LeadEndorsements.Where(l => l.Id == leid).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == leadEndorsement.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var insurerId = _context.InsurerMasters.Where(i => i.Id == leadEndorsement.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            var policyMasters = _context.PolicyClassMaster.ToList();

            endorsementDebitVM.leadEndorsement = leadEndorsement;
            endorsementDebitVM.addresss = organisation.Address;
            endorsementDebitVM.organisationName = organisation.OrganisationName;
            endorsementDebitVM.amountReceived = "";
            endorsementDebitVM.city = organisation.City;
            endorsementDebitVM.currency = "";
            endorsementDebitVM.customerAddress = client.Address;
            endorsementDebitVM.clientId = client.Id.ToString();
            endorsementDebitVM.email = organisation.Email;
            endorsementDebitVM.endorsementNo = "";

            endorsementDebitVM.paymentMode = "";
            endorsementDebitVM.phone = organisation.PhoneNumber;
            endorsementDebitVM.policyName = Util.GetPolicyName(policyMasters, leadEndorsement.PolicyClassId);
            if (string.IsNullOrEmpty(leadEndorsement.PolicyNo))
            {
                endorsementDebitVM.leadEndorsement.PolicyNo = "TBA";
            }

            endorsementDebitVM.riskType = "";

            endorsementDebitVM.branchName = branch.BranchName;
            endorsementDebitVM.customerNo = client.ClientCode.ToString();
            endorsementDebitVM.insurerName = Util.GetInsurerName(_context, insurerId);
            endorsementDebitVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            endorsementDebitVM.Client = client;

            var endorsementDebitNote = new EndorsementDebitNote();
            endorsementDebitVM.isNew = false;
            if (leadEndorsement.DebitNoteId == null || leadEndorsement.DebitNoteId == Guid.Empty)
            {
                endorsementDebitNote.PolicyNo = leadEndorsement.PolicyNo;
                endorsementDebitNote.ID = Guid.NewGuid();
                endorsementDebitNote.CreatedBy = Guid.Parse(userId);
                endorsementDebitNote.ModifiedBy = Guid.Parse(userId);
                endorsementDebitNote.LeadEndorsementId = leadEndorsement.Id;
                endorsementDebitNote.OrganisationId = orgId;
                _context.Add(endorsementDebitNote);

                leadEndorsement.DebitNoteId = endorsementDebitNote.ID;

                _context.Update(leadEndorsement);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
                endorsementDebitVM.isNew = true;

                StatusMessage = StaticContent.DEBITNOTE_GENERATE_SUCCESS_MSG;
                ViewData["StatusMessage"] = StatusMessage;
            }

            endorsementDebitVM.endorsementDebitNote = _context.EndorsementDebitNotes.Where(l => l.ID == leadEndorsement.DebitNoteId).OrderByDescending(a => a.DateCreated).SingleOrDefault();
            endorsementDebitVM.preparedBy = _context.Users.Where(x => x.Id == endorsementDebitVM.endorsementDebitNote.CreatedBy.ToString()).FirstOrDefault().FirstName;


            return PartialView("DebitNotePartial", endorsementDebitVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveLeadEndorsementPaymentDetails([FromBody]LeadEndorsementPaymentDetail lepd)
        {
            if (lepd == null)
            {
                return Json(new
                {
                    msg = "No Data"
                }
               );
            }
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

                LeadEndorsementPaymentDetail leadEndorsementPaymentDetail = new LeadEndorsementPaymentDetail();
                leadEndorsementPaymentDetail.Id = Guid.NewGuid();
                leadEndorsementPaymentDetail.Amount = lepd.Amount;
                leadEndorsementPaymentDetail.CreatedBy = Guid.Parse(userId);
                leadEndorsementPaymentDetail.DateOfPayment = lepd.DateOfPayment;
                leadEndorsementPaymentDetail.DebitNoteId = lepd.DebitNoteId;
                leadEndorsementPaymentDetail.IsDeleted = false;
                leadEndorsementPaymentDetail.LeadEndorsementId = lepd.LeadEndorsementId;
                leadEndorsementPaymentDetail.ModifiedBy = Guid.Parse(userId);
                leadEndorsementPaymentDetail.OrganisationId = orgId;

                _context.Add(leadEndorsementPaymentDetail);

                var currLead = _context.LeadEndorsements.Where(l => l.Id == lepd.LeadEndorsementId).FirstOrDefault();
                currLead.PaymentId = leadEndorsementPaymentDetail.Id;

                await _context.SaveChangesAsync();

                var paymentDetail = _context.LeadEndorsementPaymentDetails.Where(l => l.Id == leadEndorsementPaymentDetail.Id).FirstOrDefault();
                var endorsement = prepareEndorsementObject(currLead.Id);
                endorsement.CreatedBy = Guid.Parse(userId);
                endorsement.DebitNoteNo = lepd.debitNoteNo;
                endorsement.ModifiedBy = Guid.Parse(userId);

                endorsement.PaymentReceiptId = leadEndorsementPaymentDetail.Id;
                endorsement.PaymentReceiptNo = paymentDetail.ReceiptNo;

                _context.Add(endorsement);
                currLead.EndorsementId = endorsement.Id;

                _context.Update(currLead);
                await _context.SaveChangesAsync();

                StatusMessage = StaticContent.PAYMENT_GENERATE_SUCCESS_MSG;
                ViewData["StatusMessage"] = StatusMessage;

                return Json(new
                {
                    msg = "Success"
                });
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }

            return Json(new
            {
                msg = "Fail"
            });
        }

        private Endorsement prepareEndorsementObject(Guid lid)
        {
            var lead = _context.LeadEndorsements.Where(l => l.Id == lid).FirstOrDefault();

            var endorsement = new Endorsement();

            endorsement.Id = Guid.NewGuid();
            endorsement.Agents = lead.Agents;
            endorsement.ClientId = lead.ClientId;
            endorsement.Commission = lead.Commission;

            endorsement.Currency = lead.Currency;
            endorsement.DateCreated = DateTime.Now;
            endorsement.DateModified = DateTime.Now;
            endorsement.DateUpdated = DateTime.Now;
            endorsement.DebitNoteId = lead.DebitNoteId;

            endorsement.EndDate = lead.EndDate;
            endorsement.Endorsement_No = lead.EndorsementNo;
            endorsement.GrossPremium = lead.GrossPremium;
            endorsement.InsurerMasterId = lead.InsurerMasterId;
            endorsement.IsDeleted = false;
            endorsement.LeadId = lead.Id;
            endorsement.PolicyId = lead.PolicyId;

            endorsement.NetPremium = lead.NetPremium;
            endorsement.OrganisationId = lead.OrganisationId;

            endorsement.PolicyClassId = lead.PolicyClassId;
            endorsement.RiskOwnedByBroker = lead.RiskOwnedByBroker;
            endorsement.StartDate = lead.StartDate;
            endorsement.Status = lead.Status;
            endorsement.SumInsured = lead.SumInsured;
            endorsement.TranscationType = lead.TranscationType;

            return endorsement;

        }

        public ActionResult GenerateLeadEndorsementPayment(string leid, string isnew)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var leadEndorsementPaymentVM = new LeadEndorsementPaymentVM();

            LeadEndorsement leadEndorsement = _context.LeadEndorsements.Where(l => l.Id == Guid.Parse(leid)).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == leadEndorsement.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var leadEndorsementPaymentDetail = _context.LeadEndorsementPaymentDetails.Where(d => d.DebitNoteId == leadEndorsement.DebitNoteId).FirstOrDefault();
            var insurerId = _context.InsurerMasters.Where(i => i.Id == leadEndorsement.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            var policyMasters = _context.PolicyClassMaster.ToList();

            leadEndorsementPaymentVM.clientId = client.Id.ToString(); ;
            leadEndorsementPaymentVM.leadEndorsement = leadEndorsement;
            leadEndorsementPaymentVM.addresss = organisation.Address;
            leadEndorsementPaymentVM.organisationName = organisation.OrganisationName;
            leadEndorsementPaymentVM.amountReceived = "";
            leadEndorsementPaymentVM.city = organisation.City;
            leadEndorsementPaymentVM.currency = "";
            leadEndorsementPaymentVM.customerAddress = client.Address;
            leadEndorsementPaymentVM.email = organisation.Email;
            leadEndorsementPaymentVM.endorsementNo = "";
            leadEndorsementPaymentVM.leadEndorsementPaymentDetail = leadEndorsementPaymentDetail;

            leadEndorsementPaymentVM.paymentMode = "";
            leadEndorsementPaymentVM.phone = organisation.PhoneNumber;
            leadEndorsementPaymentVM.policyName = Util.GetPolicyName(policyMasters, leadEndorsement.PolicyClassId);
            leadEndorsementPaymentVM.policyNo = "TBA";
            if (string.IsNullOrEmpty(leadEndorsementPaymentVM.leadEndorsement.PolicyNo))
            {
                leadEndorsementPaymentVM.policyNo = "TBA";
            }
            else
            {
                leadEndorsementPaymentVM.policyNo = leadEndorsementPaymentVM.leadEndorsement.PolicyNo;
            }

            leadEndorsementPaymentVM.riskType = "";
            leadEndorsementPaymentVM.transcationType = "";
            leadEndorsementPaymentVM.branchName = branch.BranchName;
            leadEndorsementPaymentVM.customerNo = client.ClientCode.ToString();
            leadEndorsementPaymentVM.customerName = client.Name.ToString();
            leadEndorsementPaymentVM.preparedBy = _context.Users.Where(x => x.Id == leadEndorsementPaymentDetail.CreatedBy.ToString()).FirstOrDefault().FirstName;

            leadEndorsementPaymentVM.insurerName = Util.GetInsurerName(_context, insurerId);
            leadEndorsementPaymentVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            leadEndorsementPaymentVM.isNew = isnew;

            if (isnew == "true")
            {
                StatusMessage = StaticContent.PAYMENT_GENERATE_SUCCESS_MSG;
                ViewData["StatusMessage"] = StatusMessage;
            }
            return PartialView("EndorsementPaymentPartialView", leadEndorsementPaymentVM);

        }

        //
        public async Task<IActionResult> GetPolicyData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var clients = await _context.Clients.Where(a => a.OrganisationId == orgId).ToListAsync();
            var policies = await _context.Policies.Where(p => p.OrganisationId == orgId).ToListAsync();

            foreach (var client in clients)
            {
                client.Age = 0;

                client.Age = Util.CalculateAge(client.DateOfBirth);


            }

            foreach (var policy in policies)
            {
                
                var clientInfo = clients.Where(c => c.Id == policy.ClientId).FirstOrDefault();

                policy.Name = clientInfo.Name;
                policy.Age = clientInfo.Age;
                policy.PhoneNo = clientInfo.PhoneNo;
                policy.Email = clientInfo.Email;
                policy.Type = clientInfo.Type;

                policy.InsurerName = _context.InsurerMasters.Where(i => i.Id == policy.InsurerMasterId).FirstOrDefault().Name;

                policy.PolicyName = Util.GetPolicyName(policyClasses, policy.PolicyClassId);


            }
            var sortedList = policies.OrderBy(l => l.DateCreated).Reverse().ToList();

            policies = sortedList;

            return Json(policies);
           
        }

        public async Task<ActionResult> GetEndorsementCreditNoteData(Guid? id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var endorsementCreditVM = new EndorsementCreditNoteVM();

            Endorsement endorsement = _context.Endorsements.Where(e => e.Id == id).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == endorsement.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var endorsementPaymetDetail = _context.LeadEndorsementPaymentDetails.Where(d => d.DebitNoteId == endorsement.DebitNoteId).FirstOrDefault();
            
            var policyMasters = _context.PolicyClassMaster.ToList();

            endorsementCreditVM.endorsement = endorsement;
            endorsementCreditVM.addresss = organisation.Address;
            endorsementCreditVM.organisationName = organisation.OrganisationName;
            endorsementCreditVM.amountReceived = "";
            endorsementCreditVM.city = organisation.City;
            endorsementCreditVM.currency = "";
            endorsementCreditVM.customerAddress = client.Address;
            endorsementCreditVM.email = organisation.Email;
            endorsementCreditVM.endorsementNo = "";

            endorsementCreditVM.paymentMode = "";
            endorsementCreditVM.phone = organisation.PhoneNumber;
            endorsementCreditVM.policyName = Util.GetPolicyName(policyMasters, endorsement.PolicyClassId);
            if (string.IsNullOrEmpty(endorsementCreditVM.endorsement.PolicyNo))
            {
                endorsementCreditVM.policyNo = "TBA";
            }
            else
            {
                endorsementCreditVM.policyNo = endorsementCreditVM.endorsement.PolicyNo;
            }
            endorsementCreditVM.riskType = "";
            endorsementCreditVM.transcationType = "";
            endorsementCreditVM.branchName = branch.BranchName;
            endorsementCreditVM.customerNo = client.ClientCode.ToString();
            endorsementCreditVM.insurerName = _context.InsurerMasters.Where(i => i.Id == endorsement.InsurerMasterId).FirstOrDefault().Name;
            endorsementCreditVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            endorsementCreditVM.Client = client;

            var endorsementCreditNote = new EndorsementCreditNote();

            if (endorsement.CreditNoteId == null || endorsement.CreditNoteId == Guid.Empty)
            {
                endorsementCreditNote.PolicyNo = endorsement.PolicyNo;
                endorsementCreditNote.EndorsementNo = endorsement.EndorsementNo;
                endorsementCreditNote.Id = Guid.NewGuid();
                endorsementCreditNote.CreatedBy = Guid.Parse(userId);
                endorsementCreditNote.ModifiedBy = Guid.Parse(userId);
                endorsementCreditNote.EndorsementId = endorsement.Id;
                endorsementCreditNote.OrganisationId = orgId;
                _context.Add(endorsementCreditNote);

                endorsement.CreditNoteId = endorsementCreditNote.Id;

                _context.Update(endorsement);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            endorsementCreditVM.endorsementCreditNote = _context.EndorsementCreditNotes.Where(e => e.Id == endorsement.CreditNoteId).OrderByDescending(a => a.DateCreated).SingleOrDefault();
            endorsementCreditVM.preparedBy = _context.Users.Where(x => x.Id == endorsementCreditVM.endorsementCreditNote.CreatedBy.ToString()).FirstOrDefault().FirstName;


            return PartialView("EndorsementCreditView", endorsementCreditVM);
        }

        public async Task<IActionResult> SaveEndorsementNo([FromBody]Endorsement endorsement)
        {
           
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var selectedEndorsement = _context.Endorsements.Where(e => e.Id == endorsement.Id).FirstOrDefault();

                selectedEndorsement.DateUpdated = DateTime.Now;
                selectedEndorsement.ModifiedBy = Guid.Parse(userId);
                selectedEndorsement.DateModified = DateTime.Now;
                selectedEndorsement.EndorsementNo = endorsement.EndorsementNo;

                _context.Update(selectedEndorsement);

                var leadEndorsement = _context.LeadEndorsements.Where(l => l.Id == selectedEndorsement.LeadId).FirstOrDefault();

                leadEndorsement.Endorsement_No = selectedEndorsement.EndorsementNo;

                _context.Update(leadEndorsement);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }

                StatusMessage = StaticContent.ENDORSEMENTNO_UPDATE;
                ViewData["StatusMessage"] = StatusMessage;

                return Json(new
                {
                    msg = "Success"
                });
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }

            return Json(new
            {
                msg = "Fail"
            });
        }

        public async Task<IActionResult> UploadEndorsementReceipt(IFormFile file, IFormCollection formCollection)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //return Content("file not selected");

            var filename = Guid.NewGuid().ToString() + "_" + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Endorsement", "Receipts", filename);
            var path2 = Path.Combine("Endorsement", "Receipts", filename);

            var endorseId = formCollection["puEndorseId"].ToString();
            var endorsement = _context.Endorsements.Where(x => x.Id == Guid.Parse(endorseId)).FirstOrDefault();
            endorsement.ReceiptNo = formCollection["txtEndorseReceiptNo"].ToString();
            endorsement.ReceiptDate = Convert.ToDateTime(formCollection["txtEndorseReceiptDate"].ToString());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                endorsement.ReceiptImgUrl = path2;

                _context.Update(endorsement);
                _context.SaveChanges();

            }

            StatusMessage = StaticContent.RECEIPT_UPLOAD_SUCCESS_MSG;
            ViewData["StatusMessage"] = StatusMessage;

            //return RedirectToAction("Index");
            var routeValues = new RouteValueDictionary {
  { "id", endorsement.ClientId },
   { "IsOpen", "policies" }
};
            return RedirectToAction("Details", "Client", routeValues);
        }


    }
}
