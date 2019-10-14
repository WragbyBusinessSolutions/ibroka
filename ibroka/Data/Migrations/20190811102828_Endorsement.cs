using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class Endorsement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endorsements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsurerMasterId = table.Column<Guid>(nullable: false),

                    ClientId = table.Column<Guid>(nullable: false),
                    PolicyClassId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Endorsement_No = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),

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
                    EndorsementNo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),

                    OrganisationId = table.Column<Guid>(nullable: false),

                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false),
                    ReceiptImgUrl = table.Column<string>(nullable: true),
                    ReceiptDate= table.Column<DateTime>(nullable: true),
                    ReceiptNo= table.Column<string>(nullable: true),
                    Endorment_count = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endorsements", x => x.Id);

                    table.ForeignKey(
                        name: "FK_Endorsements_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('Endorsements', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "EndorsementId",
                table: "Policies",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EndorsementId",
                table: "LeadPolicies",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
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
        }
    }
}
