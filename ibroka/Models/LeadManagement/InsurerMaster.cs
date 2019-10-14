using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ibroka.Models.LeadManagement
{
    public class InsurerMaster:BaseClass
    {
        public Guid Id { get; set; }

        public Guid GlobalInsurerId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }        
        public string WebUrl { get; set; }
        public Decimal? Commission { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string NewRecord { get; set; }
    }
}
