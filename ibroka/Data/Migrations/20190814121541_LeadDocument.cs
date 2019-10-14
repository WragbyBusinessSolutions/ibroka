using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateTable(
             name: "LeadDocuments",
             columns: table => new
             {
                 Id = table.Column<Guid>(nullable: false),
                 FileName = table.Column<string>(nullable: true),
                 FilePath = table.Column<string>(nullable: false),
                 CreatedBy = table.Column<Guid>(nullable: false),                 
                 DateCreated = table.Column<DateTime>(nullable: false),
                 DateModified = table.Column<DateTime>(nullable: false),
                 DateUpdated = table.Column<DateTime>(nullable: false),
                 LeadId = table.Column<Guid>(nullable: false),
                 ModifiedBy = table.Column<Guid>(nullable: false),
                 OrganisationId = table.Column<Guid>(nullable: false),
                 IsDeleted = table.Column<bool>(nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_LeadDocuments", x => x.Id);
             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endorsements");

            migrationBuilder.DropColumn(
                name: "EndorsementId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "EndorsementId",
                table: "LeadPolicies");

            migrationBuilder.DropColumn(
                name: "DirectorLevel",
                table: "CorporateDirectors");
        }
    }
}
