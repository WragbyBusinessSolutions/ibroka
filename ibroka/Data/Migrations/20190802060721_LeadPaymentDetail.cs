using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadPaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "LeadPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReceiptNo = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DebitNoteId = table.Column<Guid>(nullable: true),                   
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    DateOfPayment = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)


                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadPaymentDetails_LeadPolicies_LeadId",
                        column: x => x.LeadId,
                        principalTable: "LeadPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_LeadPaymentDetails_LeadDebitNotes_DebitNoteId",
                    //    column: x => x.DebitNoteId,
                    //    principalTable: "LeadPolicyDebitNotes",
                    //    principalColumn: "Id");

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('LeadPaymentDetails', RESEED, 100000000001);");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadPaymentDetails");


        }
    }
}
