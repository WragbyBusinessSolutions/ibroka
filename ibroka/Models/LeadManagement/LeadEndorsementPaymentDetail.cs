﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class LeadEndorsementPaymentDetail:BaseClass
    {
        public Guid Id { get; set; }
        public Guid DebitNoteId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid LeadEndorsementId { get; set; }
        public DateTime DateOfPayment { get; set; }
        public decimal Amount { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ReceiptNo { get; set; }
        [NotMapped]
        public Int64 debitNoteNo { get; set; }

    }
}
