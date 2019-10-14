using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class LeadDebitNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "LeadPolicyDebitNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    //DebitNoteNo = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    PolicyNo = table.Column<string>(nullable: true),
                    EndorsementNo = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    DebitNote_No = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadPolicyDebitNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadPolicyDebitNotes_LeadPolicies_LeadId",
                        column: x => x.LeadId,
                        principalTable: "LeadPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('LeadPolicyDebitNotes', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "DebitNoteId",
                table: "LeadPolicies",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadPolicyDebitNotes");

            migrationBuilder.DropColumn(
                name: "DebitNoteId",
                table: "LeadPolicies");

        }
    }
}
