using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadEndorsement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadEndorsements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsurerMasterId = table.Column<Guid>(nullable: false),

                    ClientId = table.Column<Guid>(nullable: false),
                    PolicyClassId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    EndorsementNo = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),

                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Agents = table.Column<String>(nullable: true),
                    RiskOwnedByBroker = table.Column<String>(nullable: true),
                    SumInsured = table.Column<Decimal>(nullable: true),
                    GrossPremium = table.Column<Decimal>(nullable: true),
                    Commission = table.Column<Decimal>(nullable: true),
                    NetPremium = table.Column<Decimal>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    PolicyNo = table.Column<string>(nullable: true),
                    DebitNoteId = table.Column<Guid>(nullable: true),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false),
                    TranscationType=table.Column<string>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Endorsement_No = table.Column<string>(nullable: true),

                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadEndorsements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadEndorsements_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('LeadEndorsements', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "LeadEndorsementId",
                table: "LeadPolicies",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeadEndorsementId",
                table: "Policies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadEndorsements");

            migrationBuilder.DropColumn(
                name: "LeadEndorsementId",
                table: "LeadPolicies");

            migrationBuilder.DropColumn(
                name: "LeadEndorsementId",
                table: "Policies");
        }
    }
}
