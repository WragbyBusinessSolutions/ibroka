using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsurerMasterId = table.Column<Guid>(nullable: false),

                    ClientId = table.Column<Guid>(nullable: false),
                    PolicyClassId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    LeadNo = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),

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

                    OrganisationId = table.Column<Guid>(nullable: false),

                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadPolicies", x => x.Id);
                   

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('LeadPolicies', RESEED, 100000000001);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadPolicies");
        }
    }
}
