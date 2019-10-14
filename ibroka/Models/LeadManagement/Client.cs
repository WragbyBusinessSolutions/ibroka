//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Models.LeadManagement
{
    public class Client : BaseClass
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Int64 ClientCode { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
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
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string BVN { get; set; }
        public string CertificateOfIncorporationPath { get; set; }
        public string IncorporationOrRCNumber { get; set; }
        public string IncorporationState { get; set; }
        public DateTime? IncorporationDate { get; set; }
        public string URL { get; set; }
        public string CompanyBank { get; set; }
        public string BankBranch { get; set; }


        public string ClientPhotoPath { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }


        [NotMapped]
        public IFormFile ClientPhoto { get; set; }

        [NotMapped]
        public IFormFile CertificationDocument { get; set; }

        [NotMapped]
        public int Age { get; set; }
        [NotMapped]
        public int DaysCreated { get; set; }

    }
}
