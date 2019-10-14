using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class EndorsementDebitNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "EndorsementDebitNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),                    
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    PolicyNo = table.Column<string>(nullable: true),
                    EndorsementNo = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    LeadEndorsementId = table.Column<Guid>(nullable: false),
                    DebitNote_No = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndorsementDebitNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndorsementDebitNotes_LeadPolicies_LeadEndorsementId",
                        column: x => x.LeadEndorsementId,
                        principalTable: "LeadEndorsements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('EndorsementDebitNotes', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "DebitNoteId",
                table: "Endorsements",
                nullable: true);

            migrationBuilder.AddColumn<Int64>(
                name: "DebitNoteNo",
                table: "Endorsements",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndorsementDebitNotes");

            migrationBuilder.DropColumn(
                 name: "DebitNoteId",
                 table: "Endorsements");

            migrationBuilder.DropColumn(
                name: "DebitNoteNo",
                table: "Endorsements");

        }
    }
}
