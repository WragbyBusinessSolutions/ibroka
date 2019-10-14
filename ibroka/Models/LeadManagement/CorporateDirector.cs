using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class CorporateDirector : BaseClass
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Int64 DirectorCode { get; set; }
        public string Name { get; set; }
        public int DirectorLevel { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string TaxNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNo { get; set; }
        public DateTime? IDIssueDate { get; set; }
        public DateTime? IDExpiryDate { get; set; }
        public string IDIssuingCountry { get; set; }
        public string SourceOfIncome { get; set; }
        public string ProfilePhotoPath { get; set; }


        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

    }
}
