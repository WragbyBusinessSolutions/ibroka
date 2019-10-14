using ibroka.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    
    public class LeadPolicyDebitNote:BaseClass
    {
        public Guid ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 DebitNote_No { get; set; }
        public string PolicyNo { get; set; }
        public string EndorsementNo { get; set; }
        public Guid LeadId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        [NotMapped]
        public LeadPolicy PolicyDetail { get; set; }

        [NotMapped]
        public Organisation Organisation { get; set; }
        [NotMapped]
        public Client Client { get; set; }

    }
}
