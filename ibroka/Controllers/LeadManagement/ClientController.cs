using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ibroka.Data;
using ibroka.Models.LeadManagement;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using ibroka.Helpers;
using ibroka.ViewModel;
using System.Dynamic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
using ibroka.Models;
using ibroka.Services;

namespace ibroka.Controllers.LeadManagement
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ClientController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> GetClients()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var clientsa = _context.Clients.Where(a => a.OrganisationId == orgId).ToList();
      var leads = _context.LeadPolicies.Where(l => l.OrganisationId == orgId).ToList();

      List<Client> clients = new List<Client>();

      foreach (var item in clientsa)
      {
        if (leads.Where(x => x.ClientId == item.Id).Count() != 1)
        {
          clients.Add(item);
        }
      }


      List<Client> clientList = new List<Client>();
      Client singleClient;

      foreach (var item in clients)
      {
        singleClient = new Client();
        singleClient = item;
        singleClient.Name = item.Surname + " " + item.Name;

        clientList.Add(singleClient);
      }

      clients = Util.GetClientsAgeInYrs(clientList);
      clients = clients.OrderBy(c => c.DateCreated).Reverse().ToList();


            //clients = Util.GetClientsAgeInYrs(clients);
            //clients = clients.OrderBy(c => c.DateCreated).Reverse().ToList();

            return Json(clients);
            //return clients;
        }
        // GET: ClientMasterDetails
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            var clientVM = new ClientViewModel();

            var clientsa = _context.Clients.Where(a => a.OrganisationId == orgId).ToList();
            var leads = _context.LeadPolicies.Where(l => l.OrganisationId == orgId).ToList();

      List<Client> clients = new List<Client>();

      foreach (var item in clientsa)
      {
        if(leads.Where(x => x.ClientId == item.Id).Count() != 1)
        {
          clients.Add(item);
        }
      }

            clientVM.AllClients = clients.Count;
            clientVM.AllLeads = leads.Count;
            clientVM.Age = 0;

            clientVM.Last30Days = 0;
            clientVM.Yesterday = 0;
            clientVM.Last7Days = 0;
            clientVM.Today = 0;

            clients = Util.GetClientsAgeInYrs(clients);

            foreach (var client in clients)
            {
                if (client.DaysCreated == 0)
                {
                    clientVM.Today++;
                }
                if (client.DaysCreated == 1)
                {
                    clientVM.Yesterday++;
                }

                if (client.DaysCreated >= 1 && client.DaysCreated <= 7)
                {
                    clientVM.Last7Days++;
                }
                if (client.DaysCreated <= 30 && client.DaysCreated > 0)
                {
                    clientVM.Last30Days++;
                }
            }

            
            ViewData["StatusMessage"] = StatusMessage;
            return View(clientVM);
        }

        public Lead UpdateDirectorDetail(Lead clientDetail)
        {
            ViewBag.clientEdit = "true";
            if (clientDetail.Individual != null && clientDetail.Individual.Id != null && clientDetail.Individual.Id != Guid.Empty)
            {
                ViewBag.LeadClientId = clientDetail.Individual.Id;
                //ViewBag.clientEdit = "true";
            }


            if (clientDetail.Corporate != null && clientDetail.Corporate.Id != null && clientDetail.Corporate.Id != Guid.Empty)
            {
                ViewBag.LeadClientId = clientDetail.Corporate.Id;
                //ViewBag.clientEdit = "true";
            }
            //ViewBag.client = clientDetail;
            if (clientDetail.Director1.ProfilePhoto != null)
            {
                using (var ms = new MemoryStream())
                {
                    clientDetail.Director1.ProfilePhoto.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    clientDetail.Director1.ProfilePhotoPath = "data:" + clientDetail.Director1.ProfilePhoto.ContentType + ";base64," + Convert.ToBase64String(fileBytes);

                    // act on the Base64 data
                }
            }
            if (clientDetail.Director2.ProfilePhoto != null)
            {
                using (var ms = new MemoryStream())
                {
                    clientDetail.Director2.ProfilePhoto.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    clientDetail.Director2.ProfilePhotoPath = "data:" + clientDetail.Director2.ProfilePhoto.ContentType + ";base64," + Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            if (clientDetail.Corporate.CertificationDocument != null)
            {
                using (var ms = new MemoryStream())
                {
                    clientDetail.Corporate.CertificationDocument.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    clientDetail.Corporate.CertificateOfIncorporationPath = "data:" + clientDetail.Corporate.CertificationDocument.ContentType + ";base64," + Convert.ToBase64String(fileBytes);

                    // act on the Base64 data
                }
            }

            return clientDetail;
        }
        public async Task<bool> DeleteDirector(Guid DirectorId)
        {
            var director = await _context.CorporateDirectors.Where(a => a.Id == DirectorId).SingleOrDefaultAsync();
            if (director != null)
            {
                director.DateModified = DateTime.Now;
                director.ModifiedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                director.IsDeleted = true;
                _context.Update(director);
                await _context.SaveChangesAsync();
            }

            return true;

        }
        public IActionResult DeleteDirector1(Lead clientDetail)
        {
            if (clientDetail.Director1.Id != null && clientDetail.Director1.Id != Guid.Empty)
            {
                ViewBag.LeadClientId = clientDetail.Corporate.Id;
                //await DeleteDirector(clientDetail.Director1.Id);
                //Lead LeadDetail = await leadDetail(ViewBag.LeadClientId);
                ViewBag.clientEdit = "true";

                clientDetail.Director1 = new CorporateDirector();
                return View("Details", clientDetail);
            }
            else
            {
                clientDetail.Director1 = new CorporateDirector();
                return View("Create", clientDetail);
            }
        }
        public IActionResult DeleteDirector2(Lead clientDetail)
        {
            if (clientDetail.Director2.Id != null && clientDetail.Director2.Id != Guid.Empty)
            {
                ViewBag.LeadClientId = clientDetail.Corporate.Id;
                //  await DeleteDirector(clientDetail.Director2.Id);
                //Lead LeadDetail = await leadDetail(ViewBag.LeadClientId);
                ViewBag.clientEdit = "true";

                clientDetail.Director2 = new CorporateDirector();
                return View("Details", clientDetail);
            }
            else
            {
                clientDetail.Director2 = new CorporateDirector();

                return View("Create", clientDetail);
            }
        }

        [HttpPost]
        public IActionResult AddDirector(Lead clientDetail)
        {
            clientDetail = UpdateDirectorDetail(clientDetail);
            return View("Create", clientDetail);

        }

        [HttpPost]
        public IActionResult UpdateDirector(Lead clientDetail)
        {
            clientDetail = UpdateDirectorDetail(clientDetail);
            return View("Details", clientDetail);

        }


        public async Task<IActionResult> CreateClient(Lead clientDetail, IFormCollection formCollection)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();

            Guid ClientId = Guid.NewGuid();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            string fileName = string.Empty;
            string folderPath = string.Empty;
            string extension = string.Empty;
            if (clientDetail.Individual.Type != "Corporate")
            {


                clientDetail.Individual.Id = ClientId;
                clientDetail.Individual.DateModified = DateTime.Now;
                clientDetail.Individual.DateCreated = DateTime.Now;
                clientDetail.Individual.OrganisationId = orgId;
                clientDetail.Individual.CreatedBy = Guid.Parse(userId);
                clientDetail.Individual.ModifiedBy = Guid.Parse(userId);

                _context.Add(clientDetail.Individual);
            }
            else
            {
                clientDetail.Corporate.Id = ClientId;
                clientDetail.Corporate.Type = clientDetail.Individual.Type;
                clientDetail.Corporate.DateModified = DateTime.Now;
                clientDetail.Corporate.DateCreated = DateTime.Now;
                clientDetail.Corporate.OrganisationId = orgId;
                clientDetail.Corporate.CreatedBy = Guid.Parse(userId);
                clientDetail.Corporate.ModifiedBy = Guid.Parse(userId);


                _context.Add(clientDetail.Corporate);

                if (!string.IsNullOrEmpty(clientDetail.Director1.Name))
                {
                    clientDetail.Director1.ClientId = clientDetail.Corporate.Id;
                    clientDetail.Director1.DateModified = DateTime.Now;
                    clientDetail.Director1.DateCreated = DateTime.Now;
                    clientDetail.Director1.CreatedBy = Guid.Parse(userId);
                    clientDetail.Director1.ModifiedBy = Guid.Parse(userId);
                    clientDetail.Director1.DirectorLevel = 1;
                    _context.Add(clientDetail.Director1);
                }
                if (!string.IsNullOrEmpty(clientDetail.Director2.Name))
                {
                    clientDetail.Director2.ClientId = clientDetail.Corporate.Id;
                    clientDetail.Director2.DateModified = DateTime.Now;
                    clientDetail.Director2.DateCreated = DateTime.Now;
                    clientDetail.Director2.CreatedBy = Guid.Parse(userId);
                    clientDetail.Director2.ModifiedBy = Guid.Parse(userId);
                    clientDetail.Director2.DirectorLevel = 2;
                    _context.Add(clientDetail.Director2);
                }

            }
            await _context.SaveChangesAsync();
            await uploadImagesDocuments(clientDetail, true);

            ViewBag.LeadClientId = ClientId;
            ViewBag.clientEdit = "false";

            ///
            clientDetail.clientId = ClientId.ToString();
            ViewData["StatusMessage"] = StaticContent.CLIENT_CREAT_MESSAGE;
            return View("Create", clientDetail);

            //return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> CreateLead(Lead clientDetail, IFormCollection formCollection)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            //////save policy data
            /////
            LeadPolicy leadPolicy = new LeadPolicy();

            leadPolicy.Id = Guid.NewGuid();
            leadPolicy.ClientId = Guid.Parse(clientDetail.clientId);
            leadPolicy.Commission = clientDetail.policy.Commission;
            leadPolicy.CreatedBy = Guid.Parse(userId);
            leadPolicy.DateCreated = DateTime.Now;
            leadPolicy.DateModified = DateTime.Now;
            leadPolicy.DateUpdated = DateTime.Now;
            leadPolicy.EndDate = clientDetail.policy.EndDate;
            leadPolicy.GrossPremium = clientDetail.policy.GrossPremium;
            leadPolicy.IsDeleted = false;
            //leadPolicy.LeadNo = 1;
            leadPolicy.ModifiedBy = Guid.Parse(userId);
            leadPolicy.NetPremium = clientDetail.policy.NetPremium;
            leadPolicy.OrganisationId = orgId;
            leadPolicy.PolicyClassId = Guid.Parse(clientDetail.policyClassId);
            leadPolicy.RiskOwnedByBroker = clientDetail.policy.RiskOwnedByBroker;
            leadPolicy.StartDate = clientDetail.policy.StartDate;
            leadPolicy.Status = "New";
            leadPolicy.SumInsured = clientDetail.policy.SumInsured;
            leadPolicy.InsurerMasterId = clientDetail.policy.InsurerMasterId;
            leadPolicy.Currency = clientDetail.policy.Currency;
            leadPolicy.TranscationType = clientDetail.policy.TranscationType;
            ///get Agents           
            leadPolicy.Agents = Util.getAgents(formCollection);


            _context.Add(leadPolicy);

            ////if (clientDetail.policyClassId.ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            ////{

            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == Guid.Parse(clientDetail.policyClassId)).ToListAsync();

            foreach (LeadPolicyField lpf in leadPolicyFields)
            {
                LeadPolicyData lpd = new LeadPolicyData();
                lpd.Id = Guid.NewGuid();
                lpd.CreatedBy = Guid.Parse(userId); ;
                lpd.Data = formCollection[lpf.FieldName].ToString();
                lpd.DateCreated = DateTime.Now;
                lpd.DateModified = DateTime.Now;
                lpd.DateUpdated = DateTime.Now;
                lpd.IsDeleted = false;
                lpd.LeadPolicyFieldId = lpf.Id;
                lpd.ModifiedBy = Guid.Parse(userId); ;
                lpd.OrganisationId = orgId;
                lpd.LeadiId = leadPolicy.Id;

                _context.Add(lpd);

            }

            ////}
            await UploadLeadDocuments(formCollection, leadPolicy.Id);
            await _context.SaveChangesAsync();
            ////////
            ///
            StatusMessage = StaticContent.LEAD_CREAT_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;
            return RedirectToAction(nameof(Index));


        }
        public async Task<Lead> uploadImagesDocuments(Lead clientDetail, bool Iscreate)
        {
            FileUploadHelper fileHelper = new FileUploadHelper();
            string fileName = string.Empty;
            string folderPath = string.Empty;
            string extension = string.Empty;
            if (clientDetail.Individual != null && clientDetail.Individual.Type != "Corporate" && clientDetail.Individual.ClientPhoto != null)
            {
                extension = Path.GetExtension(clientDetail.Individual.ClientPhoto.FileName);
                fileName = fileHelper.GetUniqueNumber() + extension;
                folderPath = "images\\clientProfileImage";

                await fileHelper.UploadFile(fileName, folderPath, clientDetail.Individual.ClientPhoto, string.Empty);
                clientDetail.Individual.ClientPhotoPath = "\\" + folderPath + "\\" + fileName;
                if (Iscreate)
                {
                    _context.Update(clientDetail.Individual);

                }
            }
            else if ((clientDetail.Corporate != null && clientDetail.Corporate.Type == "Corporate") || (clientDetail.Individual != null && clientDetail.Individual.Type == "Corporate"))
            {

                if (clientDetail.Corporate.CertificationDocument != null)
                {
                    extension = Path.GetExtension(clientDetail.Corporate.CertificationDocument.FileName);
                    fileName = fileHelper.GetUniqueNumber() + extension;
                    folderPath = "leadDocuments\\incorporationCertificates";
                    await fileHelper.UploadFile(fileName, folderPath, clientDetail.Corporate.CertificationDocument, string.Empty);
                    clientDetail.Corporate.CertificateOfIncorporationPath = "\\" + folderPath + "\\" + fileName;
                    if (Iscreate)
                        _context.Update(clientDetail.Corporate);
                }
                else
                if (!string.IsNullOrEmpty(clientDetail.Corporate.CertificateOfIncorporationPath) && !clientDetail.Corporate.CertificateOfIncorporationPath.Contains("incorporationCertificates"))
                {

                    string[] fileUploaddata = getFileFregments(clientDetail.Corporate.CertificateOfIncorporationPath);

                    extension = fileUploaddata[0];
                    fileName = fileHelper.GetUniqueNumber() + "." + extension;
                    folderPath = "leadDocuments\\incorporationCertificates";
                    await fileHelper.UploadFile(fileName, folderPath, null, fileUploaddata[1]);
                    clientDetail.Corporate.CertificateOfIncorporationPath = "\\" + folderPath + "\\" + fileName;
                    if (Iscreate)
                        _context.Update(clientDetail.Corporate);
                }

                if (!string.IsNullOrEmpty(clientDetail.Director1.ProfilePhotoPath) && !clientDetail.Director1.ProfilePhotoPath.Contains("corporateDirectorImage"))
                {
                    string[] fileUploaddata = getFileFregments(clientDetail.Director1.ProfilePhotoPath);

                    extension = fileUploaddata[0];
                    fileName = fileHelper.GetUniqueNumber() + "." + extension;
                    folderPath = "images\\corporateDirectorImage";
                    await fileHelper.UploadFile(fileName, folderPath, null, fileUploaddata[1]);
                    clientDetail.Director1.ProfilePhotoPath = "\\" + folderPath + "\\" + fileName;
                    if (Iscreate)
                        _context.Update(clientDetail.Director1);

                }
                if (!string.IsNullOrEmpty(clientDetail.Director2.ProfilePhotoPath) && !clientDetail.Director2.ProfilePhotoPath.Contains("corporateDirectorImage"))
                {
                    string[] fileUploaddata = getFileFregments(clientDetail.Director2.ProfilePhotoPath);
                    extension = fileUploaddata[0];
                    fileName = fileHelper.GetUniqueNumber() + "." + extension;
                    folderPath = "images\\corporateDirectorImage";
                    await fileHelper.UploadFile(fileName, folderPath, null, fileUploaddata[1]);
                    clientDetail.Director2.ProfilePhotoPath = "\\" + folderPath + "\\" + fileName;
                    if (Iscreate)
                        _context.Update(clientDetail.Director2);
                }

            }
            if (Iscreate)
            {
                await _context.SaveChangesAsync();
            }

            return clientDetail;
        }
        public string[] getFileFregments(string encoded)
        {
            string[] uploadeddatafregment = new string[2];
            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);

            var match = regex.Match(encoded);

            var mime = match.Groups["mime"].Value;
            var encoding = match.Groups["encoding"].Value;
            var data = match.Groups["data"].Value;

            uploadeddatafregment[0] = mime.Split('/')[1];
            uploadeddatafregment[1] = data;
            return uploadeddatafregment;
        }

        public async Task<IActionResult> Details(Guid? id, string IsOpen = "false")
        {
            Lead LeadDetail = await leadDetail(id);
            if (IsOpen.ToLower() == "edit")
            {
                ViewBag.clientEdit = "true";
            }
            else
            {
                ViewBag.clientEdit = "false";
            }
            ViewBag.LeadClientId = id;
            ViewData["StatusMessage"] = StatusMessage;
            return View(LeadDetail);
        }
        // GET: ClientMasterDetails/Details/5
        //    public async Task<IActionResult> Details(Guid? id)
        //{
        //    ViewBag.clientEdit = "false";
        //    ViewBag.LeadClientId = id;

        //    Lead LeadDetail =await leadDetail(id);
        //    return View(LeadDetail);
        //}

        public async Task<Lead> leadDetail(Guid? id)
        {
            getPolicyMasterData();
            getInsurerMasterData();
            getCurrencyList();
            if (id == null)
            {
                //   return NotFound();
            }
            Lead LeadDetail = new Lead();
            var clientProfile = await _context.Clients.Where(a => a.Id == id).ToListAsync();
            LeadDetail.Individual = clientProfile.Where(a => a.Type == "Individual" && a.Id == id).SingleOrDefault();
            LeadDetail.Corporate = clientProfile.Where(a => a.Type == "Corporate" && a.Id == id).SingleOrDefault();

            var Directors = await _context.CorporateDirectors.Where(a => a.ClientId == clientProfile[0].Id && a.IsDeleted == false).OrderBy(k => k.DirectorLevel).ToListAsync();

            ///Policies data
            ///
            //order by needed on policyclassid

            //LeadDetail.leadPolicies = await _context.LeadPolicies.Where(l => l.ClientId == id).ToListAsync();
            LeadDetail.Policies = await _context.Policies.Where(p => p.ClientId == id).ToListAsync();
            LeadDetail.LeadEndorsements = await _context.LeadEndorsements.Where(p => p.ClientId == id).ToListAsync();

            var gInsurer = await _context.GlobalInsurers.Where(g => g.Id == id).ToListAsync();
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var lpData = new List<dynamic>();

            //foreach (LeadPolicy lead in LeadDetail.leadPolicies)
            //{
            //    List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == lead.PolicyClassId).ToListAsync();
            //    List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == lead.Id).ToListAsync();
            //    var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == lead.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            //    lead.InsurerName = _context.GlobalInsurers.Where(g => g.Id == globalInsurerId).FirstOrDefault().Name;

            //    var debitnote = _context.LeadPolicyDebitNotes.Where(d => d.ID == lead.DebitNoteId).FirstOrDefault();
            //    if (debitnote != null)
            //    {
            //        lead.debitNoteNo = debitnote.DebitNote_No.ToString();
            //    }

            //    lead.AgentList = Util.getAgentListFromJson(lead.Agents);
            //    lead.PolicyName = Util.GetPolicyName(policyClasses, lead.PolicyClassId);                

            //    foreach (LeadPolicyField lpf in leadPolicyFields)
            //    {
            //        dynamic DyObj = new ExpandoObject();
            //        foreach (LeadPolicyData lpd in leadsData)
            //        {
            //            if (lpd.LeadPolicyFieldId == lpf.Id)
            //            {
            //                DyObj.FieldName = lpf.FieldName;
            //                DyObj.DisplayName = lpf.DisplayName;
            //                DyObj.FieldType = lpf.FieldType;
            //                DyObj.FieldValue = lpd.Data;
            //                DyObj.FieldName = lpf.FieldName;
            //                DyObj.PolicyName = lead.PolicyName;

            //                lpData.Add(DyObj);
            //            }
            //        }

            //    }

            //    if (lead.PaymentId == Guid.Empty)
            //    {
            //        lead.payment_Id = "";
            //    }
            //    else
            //    {
            //        lead.payment_Id = lead.PaymentId.ToString();
            //    }

            //    if (lead.DebitNoteId == Guid.Empty)
            //    {
            //        lead.debitNote_Id = "";
            //    }
            //    else
            //    {
            //        lead.debitNote_Id = lead.DebitNoteId.ToString();
            //    }
            //}

            foreach (Policy policy in LeadDetail.Policies)
            {
                List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == policy.PolicyClassId).ToListAsync();
                List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == policy.LeadId).ToListAsync();
                var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == policy.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
                policy.InsurerName = Util.GetInsurerName(_context, globalInsurerId);

                policy.ReceiptImgUrl = Util.GetImageUrl(policy.ReceiptImgUrl);

                var debitnote = _context.LeadPolicyDebitNotes.Where(d => d.ID == policy.DebitNoteId).FirstOrDefault();
                if (debitnote != null)
                {
                    policy.debitNote_No = debitnote.DebitNote_No.ToString();
                }

                //policy.AgentList = Util.getAgentListFromJson(policy.Agents);
                policy.Agents = "";

                if (policy.ReceiptDate != null)
                {
                    DateTime dt = Convert.ToDateTime(policy.ReceiptDate);
                    policy.Receipt_Date = dt.ToShortDateString();
                }

                policy.PolicyName = Util.GetPolicyName(policyClasses, policy.PolicyClassId);

                if (policy.PaymentId == Guid.Empty)
                {
                    policy.payment_Id = "";
                }
                else
                {
                    policy.payment_Id = policy.PaymentId.ToString();
                }

                if (policy.DebitNoteId == Guid.Empty)
                {
                    policy.debitNote_Id = "";
                }
                else
                {
                    policy.debitNote_Id = policy.DebitNoteId.ToString();
                }
            }

            foreach(LeadEndorsement endorse in LeadDetail.LeadEndorsements)
            {
                var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == endorse.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
                endorse.PolicyName = Util.GetPolicyName(policyClasses, endorse.PolicyClassId);
                endorse.InsurerName = Util.GetInsurerName(_context, globalInsurerId);
                var endorsedebitnote = _context.EndorsementDebitNotes.Where(d => d.ID == endorse.DebitNoteId).FirstOrDefault();
                if (endorsedebitnote != null)
                {
                    endorse.debitNoteNo = endorsedebitnote.DebitNote_No.ToString();
                }
            }

            ViewBag.LeadsData = lpData;
            //LeadDetail.thirdPartyMotorPolicies = await _context.ThirdPartyMotorPolicies.Where(a => a.ClientId == id).ToListAsync();


            if (Directors.Count > 0)
            {
                if (Directors.Count == 1)
                {
                    LeadDetail.Director1 = Directors[0];
                }
                else
                {
                    LeadDetail.Director1 = Directors[0];
                    LeadDetail.Director2 = Directors[1];
                }
            }
            if (LeadDetail == null)
            {
                //     return NotFound();
            }

            LeadDetail.clientId = id.ToString();

            //var leadSorted = LeadDetail.leadPolicies.OrderBy(l => l.DateCreated).Reverse().ToList();
            var policySorted = LeadDetail.Policies.OrderBy(l => l.DateCreated).Reverse().ToList();
            var endorseSorted = LeadDetail.LeadEndorsements.OrderBy(l => l.DateCreated).Reverse().ToList();

            //LeadDetail.leadPolicies = leadSorted;
            LeadDetail.Policies = policySorted;
            LeadDetail.LeadEndorsements = endorseSorted;

            return LeadDetail;
        }

        // GET: ClientMasterDetails/Create
        public IActionResult Create()
        {
            Lead model = new Lead();
            model.clientId = "";
            getPolicyMasterData();
            getInsurerMasterData();
            getCurrencyList();
            return View(model);
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


        // POST: ClientMasterDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lead LeadDetails)
        {
            getPolicyMasterData();
            getCurrencyList();
            //if (ModelState.IsValid)
            //{
            Guid ClientId = Guid.NewGuid();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            if (LeadDetails.Individual.Type != "Corporate")
            {


                LeadDetails.Individual.Id = ClientId;
                LeadDetails.Individual.DateModified = DateTime.Now;
                LeadDetails.Individual.DateCreated = DateTime.Now;
                LeadDetails.Individual.OrganisationId = orgId;
                LeadDetails.Individual.CreatedBy = Guid.Parse(userId);
                LeadDetails.Individual.ModifiedBy = Guid.Parse(userId);
                _context.Add(LeadDetails.Individual);
            }
            else
            {
                LeadDetails.Corporate.Id = ClientId;
                LeadDetails.Corporate.Type = LeadDetails.Individual.Type;
                LeadDetails.Corporate.DateModified = DateTime.Now;
                LeadDetails.Corporate.DateCreated = DateTime.Now;
                LeadDetails.Corporate.OrganisationId = orgId;
                LeadDetails.Corporate.CreatedBy = Guid.Parse(userId);
                LeadDetails.Corporate.ModifiedBy = Guid.Parse(userId);
                _context.Add(LeadDetails.Corporate);

                if (!string.IsNullOrEmpty(LeadDetails.Director1.Name))
                {
                    LeadDetails.Director1.ClientId = LeadDetails.Corporate.Id;
                    LeadDetails.Director1.DateModified = DateTime.Now;
                    LeadDetails.Director1.DateCreated = DateTime.Now;
                    LeadDetails.Director1.CreatedBy = Guid.Parse(userId);
                    LeadDetails.Director1.ModifiedBy = Guid.Parse(userId);
                    _context.Add(LeadDetails.Director1);
                }
                if (!string.IsNullOrEmpty(LeadDetails.Director2.Name))
                {
                    LeadDetails.Director2.ClientId = LeadDetails.Corporate.Id;
                    LeadDetails.Director2.DateModified = DateTime.Now;
                    LeadDetails.Director2.DateCreated = DateTime.Now;
                    LeadDetails.Director2.CreatedBy = Guid.Parse(userId);
                    LeadDetails.Director2.ModifiedBy = Guid.Parse(userId);
                    _context.Add(LeadDetails.Director2);
                }

            }

            if (LeadDetails.policyClassId.ToLower() == "8ED74480-CA28-418A-ADD6-FE25E8E15B86".ToLower())
            {


            }
            else if (LeadDetails.policyClassId.ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            {


            }
            else if (LeadDetails.policyClassId.ToLower() == "FCF9DDF7-BF80-4503-9C5E-CECF428491EC".ToLower())
            {


            }
            else if (LeadDetails.policyClassId.ToLower() == "D2CFA791-27FC-4CEF-970E-9251FC92D120".ToLower())
            {

            }
            else if (LeadDetails.policyClassId.ToLower() == "E56249E3-E821-4969-80C4-B97BD4798CC2".ToLower())
            {


            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(LeadDetails);
        }

        public void orgDetails()
        {
            var orgdetails = _context.Organisations.Where(x => x.Id == getOrg()).FirstOrDefault();
            ViewData["OrganisationName"] = orgdetails.OrganisationName;
            ViewData["OrganisationImage"] = orgdetails.ImageUrl;
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

        // GET: ClientMasterDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientMasterDetails = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);


            if (clientMasterDetails == null)
            {
                return NotFound();
            }
            return View(clientMasterDetails);
        }


        // GET: ClientMasterDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientMasterDetails = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (clientMasterDetails == null)
            {
                return NotFound();
            }

            return View(clientMasterDetails);
        }

        // POST: ClientMasterDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clientMasterDetails = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            _context.Clients.Remove(clientMasterDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientMasterDetailsExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        public ActionResult AddPolicy(string id)
        {

            getPolicyMasterData();
            getCurrencyList();
            Lead model = new Lead();
            ViewBag.PolicyDetail = model;

            return PartialView("AddPolicyPartial");
        }

        [HttpPost]
        public ActionResult LeadDetails(string llid)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();
            string lid = "C113036E-63D8-43EE-8B4D-C8AC6A82F80F";
            Lead leadData = new Lead();

            ///Policies data
            ///
            //order by needed on policyclassid

            leadData.policy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(lid)).FirstOrDefault();

            var lpData = new List<dynamic>();

            //foreach (LeadPolicy lead in LeadDetail.leadPolicies)
            //{
            //    List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == lead.PolicyClassId).ToListAsync();
            //    List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == lead.Id).ToListAsync();
            //    var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == lead.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            //    lead.InsurerName = _context.GlobalInsurers.Where(g => g.Id == globalInsurerId).FirstOrDefault().Name;

            //    lead.AgentList = Util.getAgentListFromJson(lead.Agents);

            //    string policyName = "";
            //    if (lead.PolicyClassId.ToString().ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            //    {
            //        lead.PolicyName = "Third party motor policy";
            //    }
            //    foreach (LeadPolicyField lpf in leadPolicyFields)
            //    {
            //        dynamic DyObj = new ExpandoObject();
            //        foreach (LeadPolicyData lpd in leadsData)
            //        {
            //            if (lpd.LeadPolicyFieldId == lpf.Id)
            //            {
            //                DyObj.FieldName = lpf.FieldName;
            //                DyObj.DisplayName = lpf.DisplayName;
            //                DyObj.FieldType = lpf.FieldType;
            //                DyObj.FieldValue = lpd.Data;
            //                DyObj.FieldName = lpf.FieldName;
            //                DyObj.PolicyName = policyName;

            //                lpData.Add(DyObj);
            //            }
            //        }

            //    }
            //}

            //ViewBag.LeadsData = lpData;
            ////LeadDetail.thirdPartyMotorPolicies = await _context.ThirdPartyMotorPolicies.Where(a => a.ClientId == id).ToListAsync();



            return PartialView("LeadPartial", leadData);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Lead leadDetails)
        {

            if (ModelState.IsValid)
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
                Guid clientId = Guid.Empty;
                if (leadDetails.Individual != null && leadDetails.Individual.Type != "Corporate")
                {
                    clientId = leadDetails.Individual.Id;

                }
                else
                {
                    clientId = leadDetails.Corporate.Id;

                }
                ViewBag.LeadClientId = clientId;

                Lead updatedDetail = new Lead();
                var clientProfile = await _context.Clients.Where(a => a.Id == clientId).ToListAsync();
                updatedDetail.Individual = clientProfile.Where(a => a.Type == "Individual" && a.Id == clientId).SingleOrDefault();
                updatedDetail.Corporate = clientProfile.Where(a => a.Type == "Corporate" && a.Id == clientId).SingleOrDefault();
                var directors = await _context.CorporateDirectors.Where(a => a.ClientId == clientProfile[0].Id && a.IsDeleted == false).ToListAsync();
                //Delete Directors
                bool isDirectorDelete1 = false;
                bool isDirectorDelete2 = false;
                foreach (var dir in directors)
                {
                    bool foundFlag = false;
                    if (leadDetails.Director1 != null && dir.Id == leadDetails.Director1.Id)
                    {
                        foundFlag = true;
                    }
                    if (leadDetails.Director2 != null && dir.Id == leadDetails.Director2.Id)
                    {
                        foundFlag = true;
                    }
                    if (foundFlag == false)
                    {
                        if (dir.DirectorLevel == 1)
                        {
                            isDirectorDelete1 = true;
                        }
                        if (dir.DirectorLevel == 2)
                        {
                            isDirectorDelete2 = true;
                        }
                        await DeleteDirector(dir.Id);
                    }
                }
                directors = await _context.CorporateDirectors.Where(a => a.ClientId == clientProfile[0].Id && a.IsDeleted == false).OrderBy(k => k.DirectorLevel).ToListAsync();

                if (updatedDetail.Individual != null)
                    leadDetails.Individual.Type = updatedDetail.Individual.Type;
                else
                    leadDetails.Corporate.Type = updatedDetail.Corporate.Type;


                leadDetails = await uploadImagesDocuments(leadDetails, false);
                if (leadDetails.Individual != null && leadDetails.Individual.Type != "Corporate")
                {

                    updatedDetail.Individual.Name = leadDetails.Individual.Name;
                    updatedDetail.Individual.Title = leadDetails.Individual.Title;
                    updatedDetail.Individual.Surname = leadDetails.Individual.Surname;
                    updatedDetail.Individual.OtherNames = leadDetails.Individual.OtherNames;
                    updatedDetail.Individual.Gender = leadDetails.Individual.Gender;
                    updatedDetail.Individual.Nationality = leadDetails.Individual.Nationality;
                    updatedDetail.Individual.DateOfBirth = leadDetails.Individual.DateOfBirth;

                    updatedDetail.Individual.Occupation = leadDetails.Individual.Occupation;
                    updatedDetail.Individual.TaxNo = leadDetails.Individual.TaxNo;
                    updatedDetail.Individual.PhoneNo = leadDetails.Individual.PhoneNo;

                    updatedDetail.Individual.Email = leadDetails.Individual.Email;
                    updatedDetail.Individual.Address = leadDetails.Individual.Address;
                    updatedDetail.Individual.IdentificationType = leadDetails.Individual.IdentificationType;

                    updatedDetail.Individual.IdentificationNo = leadDetails.Individual.IdentificationNo;
                    updatedDetail.Individual.IDIssueDate = leadDetails.Individual.IDIssueDate;
                    updatedDetail.Individual.IDExpiryDate = leadDetails.Individual.IDExpiryDate;

                    updatedDetail.Individual.IDIssuingCountry = leadDetails.Individual.IDIssuingCountry;
                    updatedDetail.Individual.SourceOfIncome = leadDetails.Individual.SourceOfIncome;
                    updatedDetail.Individual.Bank = leadDetails.Individual.Bank;

                    updatedDetail.Individual.AccountNo = leadDetails.Individual.AccountNo;
                    updatedDetail.Individual.BVN = leadDetails.Individual.BVN;


                    updatedDetail.Individual.ModifiedBy = Guid.Parse(userId);
                    updatedDetail.Individual.DateModified = DateTime.Now;
                    updatedDetail.Individual.ClientPhotoPath = leadDetails.Individual.ClientPhotoPath;



                    _context.Update(updatedDetail.Individual);
                }
                else
                {
                    updatedDetail.Corporate.Name = leadDetails.Corporate.Name;
                    updatedDetail.Corporate.Title = leadDetails.Corporate.Title;
                    updatedDetail.Corporate.Address = leadDetails.Corporate.Address;
                    updatedDetail.Corporate.IncorporationOrRCNumber = leadDetails.Corporate.IncorporationOrRCNumber;

                    updatedDetail.Corporate.IncorporationState = leadDetails.Corporate.IncorporationState;
                    updatedDetail.Corporate.IncorporationDate = leadDetails.Corporate.IncorporationDate;
                    updatedDetail.Corporate.Email = leadDetails.Corporate.Email;

                    updatedDetail.Corporate.URL = leadDetails.Corporate.URL;
                    updatedDetail.Corporate.TaxNo = leadDetails.Corporate.TaxNo;
                    updatedDetail.Corporate.PhoneNo = leadDetails.Corporate.PhoneNo;

                    updatedDetail.Corporate.CompanyBank = leadDetails.Corporate.CompanyBank;
                    updatedDetail.Corporate.AccountNo = leadDetails.Corporate.AccountNo;
                    updatedDetail.Corporate.BankBranch = leadDetails.Corporate.BankBranch;
                    updatedDetail.Corporate.BVN = leadDetails.Corporate.BVN;

                    updatedDetail.Corporate.ModifiedBy = Guid.Parse(userId);
                    updatedDetail.Corporate.DateModified = DateTime.Now;

                    updatedDetail.Corporate.CertificateOfIncorporationPath = leadDetails.Corporate.CertificateOfIncorporationPath;
                    _context.Update(updatedDetail.Corporate);



                    if (directors.Count > 0)
                    {


                        if (directors.Count == 1)
                        {
                            if (directors[0].DirectorLevel == 1 && validateDirectorFieldsValue(leadDetails.Director1) && isDirectorDelete1 == false)
                            {
                                directors[0].Name = leadDetails.Director1.Name;
                                directors[0].Title = leadDetails.Director1.Title;
                                directors[0].Surname = leadDetails.Director1.Surname;
                                directors[0].OtherNames = leadDetails.Director1.OtherNames;
                                directors[0].Gender = leadDetails.Director1.Gender;
                                directors[0].Nationality = leadDetails.Director1.Nationality;
                                directors[0].DateOfBirth = leadDetails.Director1.DateOfBirth;

                                directors[0].Occupation = leadDetails.Director1.Occupation;
                                directors[0].TaxNo = leadDetails.Director1.TaxNo;
                                directors[0].PhoneNo = leadDetails.Director1.PhoneNo;

                                directors[0].Email = leadDetails.Director1.Email;
                                directors[0].Address = leadDetails.Director1.Address;
                                directors[0].IdentificationType = leadDetails.Director1.IdentificationType;

                                directors[0].IdentificationNo = leadDetails.Director1.IdentificationNo;
                                directors[0].IDIssueDate = leadDetails.Director1.IDIssueDate;
                                directors[0].IDExpiryDate = leadDetails.Director1.IDExpiryDate;

                                directors[0].IDIssuingCountry = leadDetails.Director1.IDIssuingCountry;
                                directors[0].SourceOfIncome = leadDetails.Director1.SourceOfIncome;
                                directors[0].ModifiedBy = Guid.Parse(userId);
                                directors[0].DateModified = DateTime.Now;
                                directors[0].ProfilePhotoPath = leadDetails.Director1.ProfilePhotoPath;
                                directors[0].DirectorLevel = 1;
                                _context.Update(directors[0]);
                            }

                            if (directors[0].DirectorLevel == 2 && validateDirectorFieldsValue(leadDetails.Director2) && isDirectorDelete2 == false)
                            {
                                directors[0].Name = leadDetails.Director2.Name;
                                directors[0].Title = leadDetails.Director2.Title;
                                directors[0].Surname = leadDetails.Director2.Surname;
                                directors[0].OtherNames = leadDetails.Director2.OtherNames;
                                directors[0].Gender = leadDetails.Director2.Gender;
                                directors[0].Nationality = leadDetails.Director2.Nationality;
                                directors[0].DateOfBirth = leadDetails.Director2.DateOfBirth;

                                directors[0].Occupation = leadDetails.Director2.Occupation;
                                directors[0].TaxNo = leadDetails.Director2.TaxNo;
                                directors[0].PhoneNo = leadDetails.Director2.PhoneNo;

                                directors[0].Email = leadDetails.Director2.Email;
                                directors[0].Address = leadDetails.Director2.Address;
                                directors[0].IdentificationType = leadDetails.Director2.IdentificationType;

                                directors[0].IdentificationNo = leadDetails.Director2.IdentificationNo;
                                directors[0].IDIssueDate = leadDetails.Director2.IDIssueDate;
                                directors[0].IDExpiryDate = leadDetails.Director2.IDExpiryDate;

                                directors[0].IDIssuingCountry = leadDetails.Director2.IDIssuingCountry;
                                directors[0].SourceOfIncome = leadDetails.Director2.SourceOfIncome;
                                directors[0].ModifiedBy = Guid.Parse(userId);
                                directors[0].DateModified = DateTime.Now;
                                directors[0].ProfilePhotoPath = leadDetails.Director2.ProfilePhotoPath;
                                directors[0].DirectorLevel = 2;
                                _context.Update(directors[0]);
                            }
                        }

                        else if (directors.Count == 2)
                        {
                            if (directors[0].DirectorLevel == 1 && validateDirectorFieldsValue(leadDetails.Director1) && isDirectorDelete1 == false)
                            {
                                directors[0].Name = leadDetails.Director1.Name;
                                directors[0].Title = leadDetails.Director1.Title;
                                directors[0].Surname = leadDetails.Director1.Surname;
                                directors[0].OtherNames = leadDetails.Director1.OtherNames;
                                directors[0].Gender = leadDetails.Director1.Gender;
                                directors[0].Nationality = leadDetails.Director1.Nationality;
                                directors[0].DateOfBirth = leadDetails.Director1.DateOfBirth;

                                directors[0].Occupation = leadDetails.Director1.Occupation;
                                directors[0].TaxNo = leadDetails.Director1.TaxNo;
                                directors[0].PhoneNo = leadDetails.Director1.PhoneNo;

                                directors[0].Email = leadDetails.Director1.Email;
                                directors[0].Address = leadDetails.Director1.Address;
                                directors[0].IdentificationType = leadDetails.Director1.IdentificationType;

                                directors[0].IdentificationNo = leadDetails.Director1.IdentificationNo;
                                directors[0].IDIssueDate = leadDetails.Director1.IDIssueDate;
                                directors[0].IDExpiryDate = leadDetails.Director1.IDExpiryDate;

                                directors[0].IDIssuingCountry = leadDetails.Director1.IDIssuingCountry;
                                directors[0].SourceOfIncome = leadDetails.Director1.SourceOfIncome;
                                directors[0].ModifiedBy = Guid.Parse(userId);
                                directors[0].DateModified = DateTime.Now;
                                directors[0].ProfilePhotoPath = leadDetails.Director1.ProfilePhotoPath;
                                directors[0].DirectorLevel = 1;
                                _context.Update(directors[0]);
                            }

                            if (directors[1].DirectorLevel == 2 && validateDirectorFieldsValue(leadDetails.Director2) && isDirectorDelete2 == false)
                            {
                                directors[1].Name = leadDetails.Director2.Name;
                                directors[1].Title = leadDetails.Director2.Title;
                                directors[1].Surname = leadDetails.Director2.Surname;
                                directors[1].OtherNames = leadDetails.Director2.OtherNames;
                                directors[1].Gender = leadDetails.Director2.Gender;
                                directors[1].Nationality = leadDetails.Director2.Nationality;
                                directors[1].DateOfBirth = leadDetails.Director2.DateOfBirth;

                                directors[1].Occupation = leadDetails.Director2.Occupation;
                                directors[1].TaxNo = leadDetails.Director2.TaxNo;
                                directors[1].PhoneNo = leadDetails.Director2.PhoneNo;

                                directors[1].Email = leadDetails.Director2.Email;
                                directors[1].Address = leadDetails.Director2.Address;
                                directors[1].IdentificationType = leadDetails.Director2.IdentificationType;

                                directors[1].IdentificationNo = leadDetails.Director2.IdentificationNo;
                                directors[1].IDIssueDate = leadDetails.Director2.IDIssueDate;
                                directors[1].IDExpiryDate = leadDetails.Director2.IDExpiryDate;

                                directors[1].IDIssuingCountry = leadDetails.Director2.IDIssuingCountry;
                                directors[1].SourceOfIncome = leadDetails.Director2.SourceOfIncome;
                                directors[1].ModifiedBy = Guid.Parse(userId);
                                directors[1].DateModified = DateTime.Now;
                                directors[1].ProfilePhotoPath = leadDetails.Director2.ProfilePhotoPath;
                                directors[1].DirectorLevel = 2;
                                _context.Update(directors[1]);
                            }


                        }
                    }

                    if (!string.IsNullOrEmpty(leadDetails.Director1.Name) && (directors.Count == 0 || isDirectorDelete1))
                    {
                        leadDetails.Director1.ClientId = updatedDetail.Corporate.Id;
                        leadDetails.Director1.DateModified = DateTime.Now;
                        leadDetails.Director1.ModifiedBy = Guid.Parse(userId);
                        leadDetails.Director1.CreatedBy = Guid.Parse(userId);
                        leadDetails.Director1.DateCreated = DateTime.Now;
                        leadDetails.Director1.DirectorLevel = 1;
                        if (!leadDetails.Director1.IsDeleted && validateDirectorFieldsValue(leadDetails.Director1))
                            _context.Add(leadDetails.Director1);


                    }
                    if (!string.IsNullOrEmpty(leadDetails.Director2.Name) && ((directors.Count <= 1 && directors[0].DirectorLevel != 2) || isDirectorDelete2))
                    {
                        leadDetails.Director2.ClientId = updatedDetail.Corporate.Id;
                        leadDetails.Director2.DateModified = DateTime.Now;
                        leadDetails.Director2.ModifiedBy = Guid.Parse(userId);
                        leadDetails.Director2.CreatedBy = Guid.Parse(userId);
                        leadDetails.Director2.DateCreated = DateTime.Now;
                        leadDetails.Director2.DirectorLevel = 2;
                        if (!leadDetails.Director2.IsDeleted && validateDirectorFieldsValue(leadDetails.Director2))
                            _context.Add(leadDetails.Director2);
                    }


                }

                try
                {

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException ex)
                {

                }
                // Response.Redirect("https://localhost:44319/Client/Details/" + clientId);
                //Redirect("/Client/Details/"+ clientId);
                //return RedirectToAction("Details");
            }

            Lead LeadDetail = await leadDetail(ViewBag.LeadClientId);
            ViewBag.clientEdit = "false";
            return View("Details", LeadDetail);
            //return View(LeadDetail);
            //return View(Details(ViewBag.LeadClientId));
        }


        public bool validateDirectorFieldsValue(CorporateDirector director)
        {

            if (!string.IsNullOrEmpty(director.Name) &&
        !string.IsNullOrEmpty(director.Title) &&
        !string.IsNullOrEmpty(director.Surname) &&

        !string.IsNullOrEmpty(director.Gender) &&
        !string.IsNullOrEmpty(director.Nationality) &&
        (director.DateOfBirth != null) &&

        !string.IsNullOrEmpty(director.Occupation) &&
        !string.IsNullOrEmpty(director.TaxNo) &&
        !string.IsNullOrEmpty(director.PhoneNo) &&
        !string.IsNullOrEmpty(director.PhoneNo) &&

        !string.IsNullOrEmpty(director.Email) &&
        !string.IsNullOrEmpty(director.Address) &&
        !string.IsNullOrEmpty(director.IdentificationType) &&

        !string.IsNullOrEmpty(director.IdentificationNo) &&
        (director.IDIssueDate != null) &&
        (director.IDExpiryDate != null) &&

        !string.IsNullOrEmpty(director.IDIssuingCountry) &&
        !string.IsNullOrEmpty(director.SourceOfIncome))
            {
                return true;
            }
            return false;


        }


        public async Task<IActionResult> LeadDetails1(string llid)
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

            var lpData = new List<dynamic>();


            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == leadData.policy.PolicyClassId).ToListAsync();
            List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == leadData.policy.Id).ToListAsync();

            leadData.policy.AgentList = Util.getAgentListFromJson(leadData.policy.Agents);

            string policyName = "";
            leadData.policyClassId = leadData.policy.PolicyClassId.ToString();

            if (leadData.policy.PolicyClassId.ToString().ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            {
                leadData.policy.PolicyName = "Third party motor policy";
            }
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

            leadData.documentCount=(await _context.LeadDocuments.Where(a => a.LeadId == Guid.Parse(lid) && a.IsDeleted == false).ToListAsync()).Count();
            
            ViewBag.LeadsData = lpData;



            return PartialView("LeadEditPartial", leadData);

        }

        public async Task<IActionResult> LeadEditDetails(string llid)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();
            string lid = llid;

            Lead leadData = new Lead();
            leadData.viewType = "editlead";
            ///Policies data
            ///
            //order by needed on policyclassid

            leadData.policy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(lid)).FirstOrDefault();
            leadData.startDate = leadData.policy.StartDate.ToString("yyyy-MM-dd");
            leadData.endDate = leadData.policy.EndDate.ToString("yyyy-MM-dd");

            var lpData = new List<dynamic>();


            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == leadData.policy.PolicyClassId).ToListAsync();
            List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == leadData.policy.Id).ToListAsync();

            leadData.policy.AgentList = Util.getAgentListFromJson(leadData.policy.Agents);

            string policyName = "";
            leadData.policyClassId = leadData.policy.PolicyClassId.ToString();

            if (leadData.policy.PolicyClassId.ToString().ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            {
                leadData.policy.PolicyName = "Third party motor policy";
            }
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


            return PartialView("LeadEditPartial", leadData);

        }


        public async Task<IActionResult> EditLeadData(Lead leadDetail, IFormCollection formCollection)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            leadDetail.policy.Agents = Util.getAgents(formCollection);
            leadDetail.policy.DateModified = DateTime.Now;
            leadDetail.policy.ModifiedBy = Guid.Parse(userId);
            leadDetail.policy.StartDate = Convert.ToDateTime(formCollection["txtStartDate"].ToString());
            leadDetail.policy.EndDate = Convert.ToDateTime(formCollection["txtEndDate"].ToString());
            leadDetail.policy.DebitNoteId = Guid.Empty;
            leadDetail.policy.PaymentId = Guid.Empty;

            _context.Update(leadDetail.policy);
            List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == leadDetail.policy.Id).ToListAsync();

            //if (leadDetail.policyClassId.ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
            //{

            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == leadDetail.policy.PolicyClassId).ToListAsync();

            foreach (LeadPolicyField lpf in leadPolicyFields)
            {
                LeadPolicyData leadData = leadsData.Where(l => l.LeadPolicyFieldId == lpf.Id).FirstOrDefault();

                leadData.Data = formCollection[lpf.FieldName].ToString();

                _context.Update(leadData);

            }

            //}
            await UploadLeadDocuments(formCollection, leadDetail.policy.Id);
            await _context.SaveChangesAsync();
            //////

            StatusMessage = StaticContent.LEAD_UPDATE_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;
            var routeValues = new RouteValueDictionary {
  { "id", leadDetail.policy.ClientId },
  { "IsOpen", "leads" }
};
            return RedirectToAction("Details", "Client", routeValues);

        }


        //
        public ActionResult AddNewLead(string cid)
        {
            getInsurerMasterData();
            getPolicyMasterData();
            getCurrencyList();

            Lead leadData = new Lead();
            leadData.clientId = cid;
            leadData.viewType = "addlead";
            return PartialView("AddLeadPartial", leadData);

        }


        //
        public async Task<IActionResult> SaveLead(Lead leadDetail, IFormCollection formCollection)
        {
            Guid ClientId = Guid.Parse(leadDetail.clientId);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            LeadPolicy leadPolicy = new LeadPolicy();

            leadPolicy.Id = Guid.NewGuid();
            leadPolicy.ClientId = ClientId;
            leadPolicy.Commission = leadDetail.policy.Commission;
            leadPolicy.CreatedBy = Guid.Parse(userId);
            leadPolicy.DateCreated = DateTime.Now;
            leadPolicy.DateModified = DateTime.Now;
            leadPolicy.DateUpdated = DateTime.Now;
            leadPolicy.EndDate = leadDetail.policy.EndDate;
            leadPolicy.GrossPremium = leadDetail.policy.GrossPremium;
            leadPolicy.IsDeleted = false;
            //leadPolicy.LeadNo = 1;
            leadPolicy.ModifiedBy = Guid.Parse(userId);
            leadPolicy.NetPremium = leadDetail.policy.NetPremium;
            leadPolicy.OrganisationId = orgId;
            leadPolicy.PolicyClassId = Guid.Parse(leadDetail.policyClassId);
            leadPolicy.RiskOwnedByBroker = leadDetail.policy.RiskOwnedByBroker;
            leadPolicy.StartDate = leadDetail.policy.StartDate;
            leadPolicy.Status = "New";
            leadPolicy.SumInsured = leadDetail.policy.SumInsured;
            leadPolicy.InsurerMasterId = leadDetail.policy.InsurerMasterId;
            leadPolicy.Currency = leadDetail.policy.Currency;
            leadPolicy.TranscationType = leadDetail.policy.TranscationType;
            ///get Agents           
            leadPolicy.Agents = Util.getAgents(formCollection);


            _context.Add(leadPolicy);

            //if (leadDetail.policyClassId.ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower() || leadDetail.policyClassId.ToLower() == "FCF9DDF7-BF80-4503-9C5E-CECF428491EC".ToLower())
            //{

            List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == Guid.Parse(leadDetail.policyClassId)).ToListAsync();

            foreach (LeadPolicyField lpf in leadPolicyFields)
            {
                LeadPolicyData lpd = new LeadPolicyData();
                lpd.Id = Guid.NewGuid();
                lpd.CreatedBy = Guid.Parse(userId); ;
                lpd.Data = formCollection[lpf.FieldName].ToString();
                lpd.DateCreated = DateTime.Now;
                lpd.DateModified = DateTime.Now;
                lpd.DateUpdated = DateTime.Now;
                lpd.IsDeleted = false;
                lpd.LeadPolicyFieldId = lpf.Id;
                lpd.ModifiedBy = Guid.Parse(userId); ;
                lpd.OrganisationId = orgId;
                lpd.LeadiId = leadPolicy.Id;

                _context.Add(lpd);

            }
            await UploadLeadDocuments(formCollection, leadPolicy.Id);
            //}
            try
            {
                await _context.SaveChangesAsync();
                //////
            }catch(Exception ex)
            {
                string aa = ex.Message;
            }

            //return RedirectToAction(nameof(Index));
            StatusMessage = StaticContent.LEAD_CREAT_MESSAGE;
            ViewData["StatusMessage"] = StatusMessage;

            var routeValues = new RouteValueDictionary {
  { "id", ClientId },
  { "IsOpen", "leads" }

};
            return RedirectToAction("Details", "Client", routeValues);

        }
        public async Task UploadLeadDocuments(IFormCollection formCollection, Guid leadId)
        {
            var documents = formCollection.Keys.Where(k => k.StartsWith("hdndocument")).ToDictionary(k => k, k => formCollection[k]);
            List<LeadDocument> uploadDocuments = await _context.LeadDocuments.Where(a => a.LeadId == leadId && a.IsDeleted==false).ToListAsync();
            FileUploadHelper fileHelper = new FileUploadHelper();
            Guid ClientId = Guid.NewGuid();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            foreach (var serverdoc in uploadDocuments)
            {
                bool isdeleted = true;
                foreach (var doc in documents)
                {

                    if (!doc.Value.ToString().Contains(";base64,") && serverdoc.FilePath == doc.Value.ToString())
                    {
                        isdeleted = false;
                        break;
                    }
                }
                if (isdeleted)
                {
                    serverdoc.ModifiedBy = Guid.Parse(userId);
                    serverdoc.DateModified = DateTime.Now;
                    serverdoc.IsDeleted = true;
                    _context.Update(serverdoc);
                }
            }

            foreach (var doc in documents)
            {
                if (doc.Value.ToString().Contains(";base64,"))
                {
                    LeadDocument uploadDocument = new LeadDocument();
                    string fileName = doc.Value.ToString().Substring(0, doc.Value.ToString().IndexOf("#"));
                    string fileExt = fileName.ToString().Substring(fileName.ToString().IndexOf("."));
                    string fileNameWithoutExt = fileName.ToString().Substring(0, fileName.ToString().IndexOf("."));
                    string base64 = doc.Value.ToString().Substring(doc.Value.ToString().IndexOf("#") + 1);
                    string uploadfileName = fileNameWithoutExt + "_" + fileHelper.GetUniqueNumber() + fileExt;
                    string[] fileUploaddata = getFileFregments(base64);
                    string folderPath = "leaddocuments\\leadPolicyDocuments";
                    await fileHelper.UploadFile(uploadfileName, folderPath, null, fileUploaddata[1]);
                    uploadDocument.Id = Guid.NewGuid();
                    uploadDocument.OrganisationId = orgId;
                    uploadDocument.DateModified = DateTime.Now;
                    uploadDocument.DateCreated = DateTime.Now;
                    uploadDocument.CreatedBy = Guid.Parse(userId);
                    uploadDocument.ModifiedBy = Guid.Parse(userId);
                    uploadDocument.FileName = fileName;
                    uploadDocument.FilePath = "\\" + folderPath + "\\" + uploadfileName;
                    uploadDocument.LeadId = leadId;
                    _context.Add(uploadDocument);
                }


            }
            //await _context.SaveChangesAsync();

        }
        public async Task<IActionResult> Policy()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            var clientVM = new ClientViewModel();

            clientVM.clients = await _context.Clients.Where(a => a.OrganisationId == orgId).ToListAsync();
            var leads = await _context.LeadPolicies.Where(l => l.OrganisationId == orgId).ToListAsync();

            //process clients to get the different types of leads
            clientVM.HotLeads = 0;
            clientVM.WarmLeads = 0;
            clientVM.CoolLeads = 0;
            clientVM.ColdLeads = 0;
            clientVM.AllLeads = 0;
            clientVM.Age = 0;
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

            foreach (var client in leads)
            {
                //client.Age = 0;

                int days = Util.GetDaysBetweenCurretAndGivenDate(client.DateCreated);
                //client.Age = Util.CalculateAge(client.DateOfBirth);

                if (days <= hotLeadDays)
                {
                    clientVM.HotLeads++;
                }
                else if (days > hotLeadDays && days <= warmLeadDays)
                {
                    clientVM.WarmLeads++;
                }
                else if (days > warmLeadDays && days <= coolLeadDays)
                {
                    clientVM.CoolLeads++;
                }
                else
                {
                    clientVM.ColdLeads++;
                }

                clientVM.AllLeads++;
            }

            foreach (var client in clientVM.clients)
            {
                client.Age = 0;

                client.Age = Util.CalculateAge(client.DateOfBirth);


            }
            clientVM.clients = new List<Client>();

            //return View(await _context.Clients.Where(a => a.OrganisationId == orgId).ToListAsync());
            return View("PolicyIndex", clientVM);
        }

        public async Task<IActionResult> Lead()
        {
            LeadVM leadVM = await GetLeadDashboardData();

            return View("LeadIndex", leadVM);
        }

        public async Task<IActionResult> GetLeadsData()
        {
            LeadVM leadVM = await GetLeadDashboardData();
            
            return Json(leadVM.leads);
            //return clients;
        }




    public IActionResult Customer()
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
      var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

      var customers = _context.LeadPolicies.Where(x => x.OrganisationId == orgId).Include(x => x.Client);
      List<LeadPolicy> cus = new List<LeadPolicy>();


      foreach (var item in customers)
      {
        if(cus.Where(x => x.ClientId == item.ClientId).Count() < 1)
        {
          cus.Add(item);
        }
      }


      return View(cus);
    }







        private async Task<LeadVM> GetLeadDashboardData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;
            var gInsurer = await _context.GlobalInsurers.Where(g => g.Id == orgId).ToListAsync();
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var leadVM = new LeadVM();


            var clients = await _context.Clients.Where(a => a.OrganisationId == orgId).ToListAsync();
            leadVM.leads = await _context.LeadPolicies.Where(l => l.OrganisationId == orgId).ToListAsync();
            var leadEndos = await _context.LeadEndorsements.Where(l => l.OrganisationId == orgId).ToListAsync();

            //merge lead endorsement with leads to show in the view 
            foreach (LeadEndorsement le in leadEndos)
            {
                var lead = GetLeadEndorsement(le);
                leadVM.leads.Add(lead);
            }
            //process clients to get the different types of leads
            leadVM.HotLeads = 0;
            leadVM.WarmLeads = 0;
            leadVM.CoolLeads = 0;
            leadVM.ColdLeads = 0;
            leadVM.AllLeads = 0;
            leadVM.Age = 0;
            int hotLeadDays = 0;
            int warmLeadDays = 0;
            int coolLeadDays = 0;
            int coldLeadDays = 0;

            var configOptions = await _context.BussinessOperationConfigurations.Where(b => b.OrganisationId == orgId).ToListAsync();
      //var configOptions = await _context.BussinessOperationConfigurations.ToListAsync();

      try
      {
        hotLeadDays = int.Parse(configOptions.Where(c => c.Key == "HotLeads").FirstOrDefault().Value);
        warmLeadDays = int.Parse(configOptions.Where(c => c.Key == "WarmLeads").FirstOrDefault().Value);
        coolLeadDays = int.Parse(configOptions.Where(c => c.Key == "CoolLeads").FirstOrDefault().Value);
        coldLeadDays = int.Parse(configOptions.Where(c => c.Key == "ColdLeads").FirstOrDefault().Value);

      }
      catch
      {
        hotLeadDays = 0;
        warmLeadDays = 0;
        coolLeadDays = 0;
        coldLeadDays = 0;

      }

            foreach (var client in clients)
            {
                client.Age = 0;
        client.Name = client.Surname + " " + client.Name;
                client.Age = Util.CalculateAge(client.DateOfBirth);


            }

            foreach (var lead in leadVM.leads)
            {
                //client.Age = 0;
                if (string.IsNullOrEmpty(lead.leadType))
                {
                    lead.leadType = "Lead";
                }
                int days = Util.GetDaysBetweenCurretAndGivenDate(lead.DateCreated);
                //client.Age = Util.CalculateAge(client.DateOfBirth);

                if (days <= hotLeadDays)
                {
                    leadVM.HotLeads++;
                }
                else if (days > hotLeadDays && days <= warmLeadDays)
                {
                    leadVM.WarmLeads++;
                }
                else if (days > warmLeadDays && days <= coolLeadDays)
                {
                    leadVM.CoolLeads++;
                }
                else
                {
                    leadVM.ColdLeads++;
                }

                leadVM.AllLeads++;

                var clientInfo = clients.Where(c => c.Id == lead.ClientId).FirstOrDefault();

                lead.Name = clientInfo.Name;
                lead.Age = clientInfo.Age;
                lead.PhoneNo = clientInfo.PhoneNo;
                lead.Email = clientInfo.Email;
                lead.Type = clientInfo.Type;

                var globalInsurerId = _context.InsurerMasters.Where(i => i.Id == lead.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
                lead.InsurerName = _context.GlobalInsurers.Where(g => g.Id == globalInsurerId).FirstOrDefault().Name;


                if (lead.PolicyClassId.ToString().ToLower() == "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".ToLower())
                {
                    lead.PolicyName = policyClasses.Where(p => p.Id == lead.PolicyClassId).FirstOrDefault().Name;
                }
                else if (lead.PolicyClassId.ToString().ToLower() == "FCF9DDF7-BF80-4503-9C5E-CECF428491EC".ToLower())
                {
                    lead.PolicyName = policyClasses.Where(p => p.Id == lead.PolicyClassId).FirstOrDefault().Name;
                }
                else if (lead.PolicyClassId.ToString().ToLower() == "8ED74480-CA28-418A-ADD6-FE25E8E15B86".ToLower())
                {
                    lead.PolicyName = policyClasses.Where(p => p.Id == lead.PolicyClassId).FirstOrDefault().Name;
                }
                else if (lead.PolicyClassId.ToString().ToLower() == "D2CFA791-27FC-4CEF-970E-9251FC92D120".ToLower())
                {
                    lead.PolicyName = policyClasses.Where(p => p.Id == lead.PolicyClassId).FirstOrDefault().Name;
                }
                else if (lead.PolicyClassId.ToString().ToLower() == "E56249E3-E821-4969-80C4-B97BD4798CC2".ToLower())
                {
                    lead.PolicyName = policyClasses.Where(p => p.Id == lead.PolicyClassId).FirstOrDefault().Name;
                }

            }
            var sortedList = leadVM.leads.OrderBy(l => l.DateCreated).Reverse().ToList();
            leadVM.leads = sortedList;

            return leadVM;
        }

        [HttpPost]
        public async Task<IActionResult> SaveLeadPaymentDetails([FromBody]LeadPaymentDetail lpd)
        {
            if (lpd == null)
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

                LeadPaymentDetail leadPaymentDetail = new LeadPaymentDetail();
                leadPaymentDetail.Id = Guid.NewGuid();
                leadPaymentDetail.Amount = lpd.Amount;
                leadPaymentDetail.CreatedBy = Guid.Parse(userId);
                leadPaymentDetail.DateOfPayment = lpd.DateOfPayment;
                leadPaymentDetail.DebitNoteId = lpd.DebitNoteId;
                leadPaymentDetail.IsDeleted = false;
                leadPaymentDetail.LeadId = lpd.LeadId;
                leadPaymentDetail.ModifiedBy = Guid.Parse(userId);
                leadPaymentDetail.OrganisationId = orgId;

                _context.Add(leadPaymentDetail);

                var currLead = _context.LeadPolicies.Where(l => l.Id == lpd.LeadId).FirstOrDefault();
                currLead.PaymentId = leadPaymentDetail.Id;

                await _context.SaveChangesAsync();

                var paymentDetail = _context.LeadPaymentDetails.Where(l => l.Id == leadPaymentDetail.Id).FirstOrDefault();
                var policy = preparePolicyObject(currLead.Id);
                policy.CreatedBy = Guid.Parse(userId);
                policy.DebitNoteNo = lpd.debitNoteNo;
                policy.ModifiedBy = Guid.Parse(userId);

                policy.PaymentReceiptId = leadPaymentDetail.Id;
                policy.PaymentReceiptNo = paymentDetail.ReceiptNo;

                _context.Add(policy);
                currLead.PolicyId = policy.Id;

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
        [HttpPost]
        public async Task<IActionResult> sendPaymentRecieptEmail([FromBody]LeadPaymentVM paymentReceiptPdf)
        {
            try
            {
                LeadPolicy leadPolicy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(paymentReceiptPdf.leadPolicyId)).FirstOrDefault();
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

                var leadPaymentDetail = _context.LeadPaymentDetails.Where(d => d.DebitNoteId == leadPolicy.DebitNoteId).FirstOrDefault();
                string message = StaticContent.PAYMENTRECEIPT_BODY_TEMPLATE.Replace("@reciever", clientName)
                    .Replace("@policyname", policyName)
                    .Replace("@agentname", agentName)
                    .Replace("@organization", agent.OrganisationName)
                    .Replace("@Greet", client.Type == "Individual" ? "Dear" : "M/s")
                    .Replace("@currency", leadPolicy.Currency)
                    .Replace("@receiptamount", Convert.ToString(leadPaymentDetail.Amount))
                    .Replace("@receiptdate", leadPaymentDetail.DateOfPayment.ToShortDateString());



                string paymentreceiptFile = paymentReceiptPdf.paymentReceiptBase64.Replace("data:application/pdf;base64,", "");
                await _emailSender.SendGridEmailWithAttachmentAsync(clientEmail, directorsEmails, StaticContent.PAYMENTRECEIPT_SUBJECT_TEMPLATE.Replace("@policyname", policyName), message, paymentreceiptFile, "paymentreceipt.pdf");
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
        public ActionResult GenerateLeadPayment(string lid, string isnew)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orgId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().OrganisationId;

            var leadPaymentVM = new LeadPaymentVM();

            LeadPolicy leadPolicy = _context.LeadPolicies.Where(l => l.Id == Guid.Parse(lid)).FirstOrDefault();

            Client client = _context.Clients.Where(c => c.Id == leadPolicy.ClientId).FirstOrDefault();

            Branch branch = _context.Branches.Where(b => b.OrganisationId == orgId).FirstOrDefault();

            Organisation organisation = _context.Organisations.Where(o => o.Id == orgId).FirstOrDefault();

            var leadPaymentDetail = _context.LeadPaymentDetails.Where(d => d.DebitNoteId == leadPolicy.DebitNoteId).FirstOrDefault();
            var insurerId = _context.InsurerMasters.Where(i => i.Id == leadPolicy.InsurerMasterId).FirstOrDefault().GlobalInsurerId;
            var policyMasters = _context.PolicyClassMaster.ToList();

            leadPaymentVM.clientId = client.Id.ToString(); ;
            leadPaymentVM.leadPolicy = leadPolicy;
            leadPaymentVM.addresss = organisation.Address;
            leadPaymentVM.organisationName = organisation.OrganisationName;
            leadPaymentVM.amountReceived = "";
            leadPaymentVM.city = organisation.City;
            leadPaymentVM.currency = "";
            leadPaymentVM.customerAddress = client.Address;
            leadPaymentVM.email = organisation.Email;
            leadPaymentVM.endorsementNo = "";
            leadPaymentVM.leadPaymentDetail = leadPaymentDetail;
            leadPaymentVM.leadPolicy = leadPolicy;
            leadPaymentVM.paymentMode = "";
            leadPaymentVM.phone = organisation.PhoneNumber;
            leadPaymentVM.policyName = Util.GetPolicyName(policyMasters, leadPolicy.PolicyClassId);
            leadPaymentVM.policyNo = "TBA";
            if (string.IsNullOrEmpty(leadPaymentVM.leadPolicy.PolicyNo))
            {
                leadPaymentVM.policyNo = "TBA";
            }
            else
            {
                leadPaymentVM.policyNo = leadPaymentVM.leadPolicy.PolicyNo;
            }

            leadPaymentVM.riskType = "";
            leadPaymentVM.transcationType = "";
            leadPaymentVM.branchName = branch.BranchName;
            leadPaymentVM.customerNo = client.ClientCode.ToString();
            leadPaymentVM.customerName = client.Name.ToString();
            leadPaymentVM.preparedBy = _context.Users.Where(x => x.Id == leadPaymentDetail.CreatedBy.ToString()).FirstOrDefault().FirstName;

            leadPaymentVM.insurerName = Util.GetInsurerName(_context, insurerId);
            leadPaymentVM.imageUrl = Util.GetImageUrl(organisation.ImageUrl);
            leadPaymentVM.isNew = isnew;

            if (isnew == "true")
            {
                StatusMessage = StaticContent.PAYMENT_GENERATE_SUCCESS_MSG;
                ViewData["StatusMessage"] = StatusMessage;
            }
            return PartialView("DebitNotePaymentPartial", leadPaymentVM);

        }

        private Policy preparePolicyObject(Guid lid)
        {
            var lead = _context.LeadPolicies.Where(l => l.Id == lid).FirstOrDefault();

            var policy = new Policy();

            policy.Id = Guid.NewGuid();
            policy.Agents = lead.Agents;
            policy.ClientId = lead.ClientId;
            policy.Commission = lead.Commission;

            policy.Currency = lead.Currency;
            policy.DateCreated = DateTime.Now;
            policy.DateModified = DateTime.Now;
            policy.DateUpdated = DateTime.Now;
            policy.DebitNoteId = lead.DebitNoteId;

            policy.EndDate = lead.EndDate;
            policy.EndorsementNo = lead.EndorsementNo;
            policy.GrossPremium = lead.GrossPremium;
            policy.InsurerMasterId = lead.InsurerMasterId;
            policy.IsDeleted = false;
            policy.LeadId = lead.Id;

            policy.NetPremium = lead.NetPremium;
            policy.OrganisationId = lead.OrganisationId;

            policy.PolicyClassId = lead.PolicyClassId;
            policy.RiskOwnedByBroker = lead.RiskOwnedByBroker;
            policy.StartDate = lead.StartDate;
            policy.Status = lead.Status;
            policy.SumInsured = lead.SumInsured;
            policy.TranscationType = lead.TranscationType;

            return policy;

        }

        private LeadPolicy GetLeadEndorsement(LeadEndorsement le)
        {
            LeadPolicy lp = new LeadPolicy();
            lp.ClientId = le.ClientId;
            lp.DateCreated =le.DateCreated ;
            lp.Id = le.Id;
            lp.InsurerMasterId = le.InsurerMasterId;
            lp.PolicyClassId = le.PolicyClassId;
            lp.SumInsured = le.SumInsured;
            lp.leadType = "Endorsement";
            lp.DebitNoteId = le.DebitNoteId;
            lp.PaymentId = le.PaymentId;

            var debitNote = _context.EndorsementDebitNotes.Where(d => d.LeadEndorsementId == le.Id).FirstOrDefault();
            if (debitNote != null)
            {
                lp.debitNoteNo = debitNote.DebitNote_No.ToString();
            }

            //lp.debitNoteNo = le.debitNoteNo;

            lp.StartDate = le.StartDate;
            lp.EndDate = le.EndDate;
            lp.GrossPremium = Convert.ToDecimal(le.GrossPremium);
            lp.Commission = Convert.ToDecimal(le.Commission);
            lp.NetPremium = le.NetPremium;


            return lp;
        }
       
        public async Task<IActionResult> GetLeadByClient(Guid? clientId)
        {
           
            var leadPolicies  = await _context.LeadPolicies.Where(l => l.ClientId == clientId).ToListAsync();
           
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var leadEndos = await _context.LeadEndorsements.Where(l => l.ClientId == clientId).ToListAsync();

            //merge lead endorsement with leads to show in the view 
            foreach (LeadEndorsement le in leadEndos)
            {
                var lead = GetLeadEndorsement(le);
                leadPolicies.Add(lead);
            }

            foreach (LeadPolicy lead in leadPolicies)
            {
                if (string.IsNullOrEmpty(lead.leadType))
                {
                    lead.leadType = "Lead";
                }
                List<LeadPolicyField> leadPolicyFields = await _context.LeadPolicyFields.Where(f => f.PolicyClassId == lead.PolicyClassId).ToListAsync();
                List<LeadPolicyData> leadsData = await _context.LeadPolicyData.Where(l => l.LeadiId == lead.Id).ToListAsync();
               
                lead.InsurerName = _context.InsurerMasters.Where(i => i.Id == lead.InsurerMasterId).FirstOrDefault().Name;

                var debitnote = _context.LeadPolicyDebitNotes.Where(d => d.ID == lead.DebitNoteId).FirstOrDefault();
                if (debitnote != null)
                {
                    lead.debitNoteNo = debitnote.DebitNote_No.ToString();
                }

                if (lead.PaymentId == Guid.Empty)
                {
                    lead.payment_Id = "";
                }
                else
                {
                    lead.payment_Id = lead.PaymentId.ToString();
                }

                if (lead.DebitNoteId == Guid.Empty)
                {
                    lead.debitNote_Id = "";
                }
                else
                {
                    lead.debitNote_Id = lead.DebitNoteId.ToString();
                }


                lead.PolicyName = Util.GetPolicyName(policyClasses, lead.PolicyClassId);                
            }

            var leadSorted = leadPolicies.OrderBy(l => l.DateCreated).Reverse().ToList();
           
            leadPolicies = leadSorted;
            var aa= Json(leadPolicies);
            return Json(leadPolicies);
        }

        public async Task<IActionResult> GetPolicyByClient(Guid? clientId)
        {
            var policies = await _context.Policies.Where(p => p.ClientId == clientId).ToListAsync();
            
            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            foreach (Policy policy in policies)
            {

                policy.InsurerName = _context.InsurerMasters.Where(i => i.Id == policy.InsurerMasterId).FirstOrDefault().Name;

                policy.ReceiptImgUrl = Util.GetImageUrl(policy.ReceiptImgUrl);

                var debitnote = _context.LeadPolicyDebitNotes.Where(d => d.ID == policy.DebitNoteId).FirstOrDefault();
                if (debitnote != null)
                {
                    policy.debitNote_No = debitnote.DebitNote_No.ToString();
                }

                //policy.AgentList = Util.getAgentListFromJson(policy.Agents);
                policy.Agents = "";

                if (policy.ReceiptDate != null)
                {
                    DateTime dt = Convert.ToDateTime(policy.ReceiptDate);
                    policy.Receipt_Date = dt.ToShortDateString();
                }

                policy.PolicyName = Util.GetPolicyName(policyClasses, policy.PolicyClassId);

                if (policy.PaymentId == Guid.Empty)
                {
                    policy.payment_Id = "";
                }
                else
                {
                    policy.payment_Id = policy.PaymentId.ToString();
                }

                if (policy.DebitNoteId == Guid.Empty)
                {
                    policy.debitNote_Id = "";
                }
                else
                {
                    policy.debitNote_Id = policy.DebitNoteId.ToString();
                }

                if (policy.CreditNoteId == Guid.Empty)
                {
                    policy.CreditNote_Id = "";
                }
                else
                {
                    policy.CreditNote_Id = policy.CreditNoteId.ToString();
                }

                if (string.IsNullOrEmpty(policy.PolicyNo))
                {
                    policy.PolicyNo = "";
                }

            }

            var policySorted = policies.OrderBy(l => l.DateCreated).Reverse().ToList();
            
            policies = policySorted;
           

            return Json(policies);
        }

        public async Task<IActionResult> GetEndorsementByPolicyId(Guid? pid)
        {

            var policyClasses = await _context.PolicyClassMaster.ToListAsync();

            var endorsements = await _context.Endorsements.Where(l => l.PolicyId == pid).ToListAsync();
                       

            foreach (Endorsement en in endorsements)
            {               
                
                en.InsurerName = _context.InsurerMasters.Where(i => i.Id == en.InsurerMasterId).FirstOrDefault().Name;

                var debitnote = _context.LeadPolicyDebitNotes.Where(d => d.ID == en.DebitNoteId).FirstOrDefault();
                if (debitnote != null)
                {
                    en.DebitNote_No = debitnote.DebitNote_No.ToString();
                }

                if (en.PaymentId == Guid.Empty)
                {
                    en.Payment_Id = "";
                }
                else
                {
                    en.Payment_Id = en.PaymentId.ToString();
                }

                if (en.DebitNoteId == Guid.Empty)
                {
                    en.DebitNote_Id = "";
                }
                else
                {
                    en.DebitNote_Id = en.DebitNoteId.ToString();
                }
                en.ReceiptImgUrl = Util.GetImageUrl(en.ReceiptImgUrl);

                en.PolicyName = Util.GetPolicyName(policyClasses, en.PolicyClassId);

                if (string.IsNullOrEmpty(en.EndorsementNo))
                {
                    en.EndorsementNo = string.Empty;
                }

                if (en.ReceiptDate != null)
                {
                    DateTime dt = Convert.ToDateTime(en.ReceiptDate);
                    en.Receipt_Date = dt.ToShortDateString();
                }

            }

            var endoSorted = endorsements.OrderBy(l => l.DateCreated).Reverse().ToList();

            endorsements = endoSorted;
            
            return Json(endorsements);
        }

    }
}
