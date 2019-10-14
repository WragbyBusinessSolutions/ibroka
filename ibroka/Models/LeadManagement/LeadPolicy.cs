using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.Models.LeadManagement
{
    public class LeadPolicy : BaseClass
    {
        public Guid Id { get; set; }
        public Guid InsurerMasterId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PolicyClassId { get; set; }
        public string Status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 LeadNo { get; set; }
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
        public Guid DebitNoteId { get; set; }
        public Guid PaymentId { get; set; }
        public string Currency { get; set; }
        public string PolicyNo { get; set; }
        public string EndorsementNo { get; set; }
        public Guid PolicyId { get; set; }
        public string TranscationType { get; set; }
        public Guid? EndorsementId { get; set; }
        public Guid? LeadEndorsementId { get; set; }

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
        public string debitNoteNo { get; set; }
        [NotMapped]
        public string leadType { get; set; }

        public virtual Client Client { get; set; }
    }
}
