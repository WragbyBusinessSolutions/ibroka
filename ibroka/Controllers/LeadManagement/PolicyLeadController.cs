using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E4S.Data;
using E4S.Helpers;
using E4S.Models;
using E4S.Models.LeadManagement;
using E4S.Services;
using E4S.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E4S.Controllers.LeadManagement
{
    public class PolicyLeadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public PolicyLeadController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<ActionResult> GetLeadPolicyData(Guid? lid)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var leadDebitVM = new LeadDebitVM();

            LeadPolicy leadPolicy = _context.LeadPolicies.Where(l => l.Id == lid).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == leadPolicy.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var leadPaymentDetail = _context.LeadPaymentDetails.Where(d => d.DebitNoteId == leadPolicy.DebitNoteId).FirstOrDefault();
            var insurerId = _context.InsurerMasters.Where(i => i.Id == leadPolicy.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            var policyMasters = _context.PolicyClassMaster.ToList();

            leadDebitVM.leadPolicy = leadPolicy;
            leadDebitVM.addresss = organisation.Address;
            leadDebitVM.organisationName = organisation.OrganisationName;
            leadDebitVM.amountReceived = "";
            leadDebitVM.city = organisation.City;
            leadDebitVM.currency = "";
            leadDebitVM.customerAddress = client.Address;
            leadDebitVM.clientId = client.Id.ToString();
            leadDebitVM.email = organisation.Email;
            leadDebitVM.endorsementNo = "";

            leadDebitVM.leadPolicy = leadPolicy;
            leadDebitVM.paymentMode = "";
            leadDebitVM.phone = organisation.PhoneNumber;
            leadDebitVM.policyName = Util.GetPolicyName(policyMasters, leadPolicy.PolicyClassId);
            if (string.IsNullOrEmpty(leadDebitVM.leadPolicy.PolicyNo))
            {
                leadDebitVM.policyNo = "TBA";
            }
            else
            {
                leadDebitVM.policyNo = leadDebitVM.leadPolicy.PolicyNo;
            }
            leadDebitVM.riskType = "";
            leadDebitVM.transcationType = "";
            leadDebitVM.branchName = branch.BranchName;
            leadDebitVM.customerNo = client.ClientCode.ToString();
            leadDebitVM.insurerName = Util.GetInsurerName(_context, insurerId);
            leadDebitVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            leadDebitVM.Client = client;

            var leadPolicyDebitNote = new LeadPolicyDebitNote();
            leadDebitVM.isNew = false;
            if (leadPolicy.DebitNoteId == null || leadPolicy.DebitNoteId == Guid.Empty)
            {
                leadPolicyDebitNote.PolicyNo = leadPolicy.PolicyNo;
                leadPolicyDebitNote.EndorsementNo = leadPolicy.EndorsementNo;
                leadPolicyDebitNote.ID = Guid.NewGuid();
                leadPolicyDebitNote.CreatedBy = Guid.Parse(userId);
                leadPolicyDebitNote.ModifiedBy = Guid.Parse(userId);
                leadPolicyDebitNote.LeadId = leadPolicy.Id;
                leadPolicyDebitNote.OrganisationId = orgId;
                _context.Add(leadPolicyDebitNote);

                leadPolicy.DebitNoteId = leadPolicyDebitNote.ID;

                _context.Update(leadPolicy);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
                leadDebitVM.isNew = true;

                StatusMessage = StaticContent.DEBITNOTE_GENERATE_SUCCESS_MSG;
                ViewData["StatusMessage"] = StatusMessage;
            }

            leadDebitVM.leadPolicyDebitNote = _context.LeadPolicyDebitNotes.Where(l => l.ID == leadPolicy.DebitNoteId).OrderByDescending(a => a.DateCreated).SingleOrDefault();
            leadDebitVM.preparedBy = _context.Users.Where(x => x.Id == leadDebitVM.leadPolicyDebitNote.CreatedBy.ToString()).FirstOrDefault().FirstName;


            return PartialView("DebitNotePartial", leadDebitVM);
        }
        [HttpPost]
        public async Task<IActionResult> sendEmail([FromBody]LeadDebitVM debitNotePdf)
        {
            try
            {
                LeadPolicy leadPolicy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(debitNotePdf.leadPolicyId)).FirstOrDefault();
                Client client = _context.Clients.Where(l => l.Id == leadPolicy.ClientId).FirstOrDefault();
                string clientName = string.Empty;
                string clientEmail = string.Empty;
                string policyName = string.Empty;
                string agentName = string.Empty;
                string[] directorsEmails = new string[2];
                if (client.Type == "Individual")
                {
                    clientName = client.Title + " " + client.Name + " " + client.Surname;
                }
                else
                {
                    clientName = client.Name;
                }
                clientEmail = client.Email;
                var policyMasters = _context.PolicyClassMaster.ToList();
                policyName = Util.GetPolicyName(policyMasters, leadPolicy.PolicyClassId);
                var agent = _context.Users.Where(x => x.Id == leadPolicy.CreatedBy.ToString()).FirstOrDefault();
                agentName = agent.FirstName + " " + agent.LastName;
                var directors = await _context.CorporateDirectors.Where(x => x.ClientId == leadPolicy.ClientId && x.IsDeleted == false).ToListAsync();
                if (directors != null && directors.Count() > 0)
                {
                    directorsEmails[0] = directors[0].Email;
                    if (directors.Count() == 2)
                        directorsEmails[1] = directors[1].Email;

                }


                string message = StaticContent.DEBITNOTE_BODY_TEMPLATE.Replace("@reciever", clientName)
                    .Replace("@policyname", policyName)
                    .Replace("@agentname", agentName)
                    .Replace("@organization", agent.OrganisationName)
                    .Replace("@Greet", client.Type == "Individual" ? "Dear" : "M/s");



                string debitnoteFile = debitNotePdf.debitNoteBase64.Replace("data:application/pdf;base64,", "");
                await _emailSender.SendGridEmailWithAttachmentAsync(clientEmail, directorsEmails, StaticContent.DEBITNOTE_SUBJECT_TEMPLATE.Replace("@policyname", policyName), message, debitnoteFile, "DebitNote.pdf");
                return Json(new
                {
                    msg = "Success"
                });
            }
            catch (Exception xe)
            {
                return Json(new
                {
                    msg = "Fail"
                });
            }
        }
    }
}