using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class EndorsementCreditNote : BaseClass
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 CreditNote_No { get; set; }
        public string PolicyNo { get; set; }
        public string EndorsementNo { get; set; }
        public Guid EndorsementId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        [NotMapped]
        public LeadEndorsement leadEndorsement { get; set; }

        [NotMapped]
        public Organisation Organisation { get; set; }
        [NotMapped]
        public Client Client { get; set; }
    }
}
