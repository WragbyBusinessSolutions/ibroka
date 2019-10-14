using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class CorporateDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                           name: "CorporateDirectors",
                           columns: table => new
                           {
                               Id = table.Column<Guid>(nullable: false),
                               ClientId = table.Column<Guid>(nullable: false),
                               DirectorCode = table.Column<Int64>(nullable: false),
                               Title = table.Column<string>(nullable: false),
                               DirectorLevel = table.Column<int>(nullable: false),
                               Name = table.Column<string>(nullable: true),
                               Surname = table.Column<string>(nullable: true),
                               OtherNames = table.Column<string>(nullable: true),
                               Gender = table.Column<string>(nullable: true),
                               Nationality = table.Column<string>(nullable: true),
                               DateOfBirth = table.Column<DateTime>(nullable: true),
                               Occupation = table.Column<string>(nullable: true),
                               TaxNo = table.Column<string>(nullable: true),
                               PhoneNo = table.Column<string>(nullable: true),
                               Email = table.Column<string>(nullable: true),
                               Address = table.Column<string>(nullable: true),
                               IdentificationType = table.Column<string>(nullable: true),
                               IdentificationNo = table.Column<string>(nullable: true),
                               IDIssueDate = table.Column<DateTime>(nullable: true),
                               IDExpiryDate = table.Column<DateTime>(nullable: true),
                               IDIssuingCountry = table.Column<string>(nullable: true),
                               SourceOfIncome = table.Column<string>(nullable: true),
                               ProfilePhotoPath = table.Column<string>(nullable: true),
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
                               table.PrimaryKey("PK_CorporateDirectors", x => x.Id);
                               table.ForeignKey(
                                   name: "FK_Director_Clients_ClientMasterId",
                                   column: x => x.ClientId,
                                   principalTable: "Clients",
                                   principalColumn: "Id",
                                   onDelete: ReferentialAction.Cascade);
                           });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
