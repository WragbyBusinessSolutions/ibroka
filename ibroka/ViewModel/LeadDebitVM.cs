using E4S.Models.LeadManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.ViewModel
{
    public class LeadDebitVM
    {
        public LeadPolicy leadPolicy { get; set; }
        public string policyName { get; set; }
        public LeadPolicyDebitNote leadPolicyDebitNote { get; set; }
        public Client Client { get; set; }
        public string policyNo { get; set; }
        public string endorsementNo { get; set; }
        public string customerAddress { get; set; }
        public string transcationType { get; set; }
        public string amountReceived { get; set; }
        public string currency { get; set; }
        public string riskType { get; set; }
        public string paymentMode { get; set; }
        public string addresss { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string branchName { get; set; }
        public string customerNo { get; set; }
        public string customerName { get; set; }
        public string insurerName { get; set; }
        public string imageUrl { get; set; }

        public string preparedBy { get; set; }
        public string mailContent { get; set; }
        public string organisationName { get; set; }
        public bool isNew { get; set; }
        public string clientId { get; set; }

        public string debitNoteBase64 { get; set; }
        public string leadPolicyId { get; set; }
    }
}
