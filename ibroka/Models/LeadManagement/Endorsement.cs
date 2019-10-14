using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class Endorsement:BaseClass
    {
      public Guid Id { get; set; }
       public Guid InsurerMasterId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PolicyClassId { get; set; }
        public string Status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 Endorsement_No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Agents { get; set; }
        public string RiskOwnedByBroker { get; set; }
        public decimal? SumInsured { get; set; }
        public decimal? GrossPremium { get; set; }
        public decimal? Commission { get; set; }
        public decimal? NetPremium { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid PaymentId { get; set; }
        public string Currency { get; set; }
        public string PolicyNo { get; set; }
        public string EndorsementNo { get; set; }
        public string Description { get; set; }
        public Guid LeadId { get; set; }
        public Guid PolicyId { get; set; }
        public Guid DebitNoteId { get; set; }
        public string TranscationType { get; set; }
        public Int64 DebitNoteNo { get; set; }
        public Guid PaymentReceiptId { get; set; }
        public Int64 PaymentReceiptNo { get; set; }
        public Guid? CreditNoteId { get; set; }
        public Int64 CreditNoteNo { get; set; }
        public string ReceiptImgUrl { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public int Endorment_count { get; set; }

        [NotMapped]
        public string InsurerName { get; set; }
        [NotMapped]
        public string DebitNote_No { get; set; }

        [NotMapped]
        public string Payment_Id { get; set; }
        [NotMapped]
        public string DebitNote_Id { get; set; }
        [NotMapped]
        public string PolicyName { get; set; }
        [NotMapped]
        public string Receipt_Date { get; set; }

    }
}
