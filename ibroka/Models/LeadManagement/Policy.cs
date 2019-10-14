using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class Policy : BaseClass
    {
        public Guid Id { get; set; }
        public Guid InsurerMasterId { get; set; }

        public Guid ClientId { get; set; }
        public Guid PolicyClassId { get; set; }
        public string Status { get; set; }
        public Guid LeadId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 PolicyCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Agents { get; set; }
        public string RiskOwnedByBroker { get; set; }
        public Decimal SumInsured { get; set; }
        public Decimal GrossPremium { get; set; }
        public Decimal Commission { get; set; }
        public Decimal NetPremium { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid PaymentId { get; set; }
        public string Currency { get; set; }
        public string PolicyNo { get; set; }
        public string EndorsementNo { get; set; }
        public Guid PaymentReceiptId { get; set; }
        public Int64 PaymentReceiptNo { get; set; }
        public Int64 DebitNoteNo { get; set; }
        public Guid DebitNoteId { get; set; }
        public Guid CreditNoteId { get; set; }
        public Int64? CreditNoteNo { get; set; }
        public string ReceiptImgUrl { get; set; }
        public string TranscationType { get; set; }
        public Guid? EndorsementId { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public Guid? LeadEndorsementId { get; set; }
        public int? EndorsementCount { get; set; }

        [NotMapped]
        public string PolicyName { get; set; }
        [NotMapped]
        public string InsurerName { get; set; }
        [NotMapped]
        public List<Agent> AgentList { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public int Age { get; set; }
        [NotMapped]
        public string PhoneNo { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public string debitNote_Id { get; set; }
        [NotMapped]
        public string payment_Id { get; set; }
        [NotMapped]
        public string debitNote_No { get; set; }
        [NotMapped]
        public string Receipt_Date { get; set; }
        [NotMapped]
        public string CreditNote_Id { get; set; }
    }
}
