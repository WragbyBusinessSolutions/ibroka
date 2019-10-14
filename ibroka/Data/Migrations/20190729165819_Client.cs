using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class Client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                   name: "Clients",
                   columns: table => new
                   {
                       Id = table.Column<Guid>(nullable: false),
                       ClientCode = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                       AccountNo = table.Column<string>(nullable: true),
                       Address = table.Column<string>(nullable: true),
                       BVN = table.Column<string>(nullable: true),
                       Bank = table.Column<string>(nullable: true),
                       BankBranch = table.Column<string>(nullable: true),
                       CompanyBank = table.Column<string>(nullable: true),
                       DateOfBirth = table.Column<DateTime>(nullable: true),
                       Email = table.Column<string>(nullable: true),
                       Gender = table.Column<string>(nullable: true),
                       IDExpiryDate = table.Column<DateTime>(nullable: true),
                       IDIssueDate = table.Column<DateTime>(nullable: true),
                       IDIssuingCountry = table.Column<string>(nullable: true),
                       IdentificationNo = table.Column<string>(nullable: true),
                       IdentificationType = table.Column<string>(nullable: true),
                       IncorporationDate = table.Column<DateTime>(nullable: true),
                       IncorporationOrRCNumber = table.Column<string>(nullable: true),
                       IncorporationState = table.Column<string>(nullable: true),
                       Name = table.Column<string>(nullable: true),
                       Title = table.Column<string>(nullable: true),
                       Nationality = table.Column<string>(nullable: true),
                       Occupation = table.Column<string>(nullable: true),
                       OtherNames = table.Column<string>(nullable: true),
                       PhoneNo = table.Column<string>(nullable: true),
                       SourceOfIncome = table.Column<string>(nullable: true),
                       Surname = table.Column<string>(nullable: true),
                       TaxNo = table.Column<string>(nullable: true),
                       Type = table.Column<string>(nullable: true),
                       URL = table.Column<string>(nullable: true),
                       CertificateOfIncorporationPath = table.Column<string>(nullable: true),
                       ClientPhotoPath = table.Column<string>(nullable: true),
                       DateUpdated = table.Column<DateTime>(nullable: true),
                       DateModified = table.Column<DateTime>(nullable: true),
                       DateCreated = table.Column<DateTime>(nullable: true),
                       OrganisationId = table.Column<Guid>(nullable: true),
                       CreatedBy = table.Column<Guid>(nullable: true),
                       ModifiedBy = table.Column<Guid>(nullable: true),
                       IsDeleted = table.Column<bool>(nullable: true),
                   },
                   constraints: table =>
                   {
                       table.PrimaryKey("PK_Clients", x => x.Id);
                   });
            migrationBuilder.Sql("DBCC CHECKIDENT ('Clients', RESEED, 100000000001);");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
