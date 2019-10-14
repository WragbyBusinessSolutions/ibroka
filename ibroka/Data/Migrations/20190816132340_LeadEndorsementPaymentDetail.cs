using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadEndorsementPaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "LeadEndorsementPaymentDetails",
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
                    LeadEndorsementId = table.Column<Guid>(nullable: false),
                    DateOfPayment = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadEndorsementPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadEndorsementPaymentDetails_LeadEndorsements_LeadEndorsementId",
                        column: x => x.LeadEndorsementId,
                        principalTable: "LeadEndorsements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);                  

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('LeadEndorsementPaymentDetails', RESEED, 100000000001);");

            migrationBuilder.AddColumn<string>(
                name: "TranscationType",
                table: "Endorsements",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentReceiptId",
                table: "Endorsements",
                nullable: true);

            migrationBuilder.AddColumn<Int64>(
                name: "PaymentReceiptNo",
                table: "Endorsements",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EndorsementId",
                table: "LeadEndorsements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadEndorsementPaymentDetails");

            migrationBuilder.DropColumn(
                name: "TranscationType",
                table: "Endorsements");

            migrationBuilder.DropColumn(
                name: "PaymentReceiptId",
                table: "Endorsements");

            migrationBuilder.DropColumn(
                name: "PaymentReceiptNo",
                table: "Endorsements");

            migrationBuilder.DropColumn(
                name: "EndorsementId",
                table: "LeadEndorsements");

        }
    }
}
