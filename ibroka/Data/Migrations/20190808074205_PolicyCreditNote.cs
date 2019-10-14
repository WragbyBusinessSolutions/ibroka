using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class PolicyCreditNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "PolicyCreditNotes",
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
                    PolicyId = table.Column<Guid>(nullable: false),
                    CreditNote_No = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCreditNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyCreditNotes_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('PolicyCreditNotes', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "CreditNoteId",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<Int64>(
               name: "CreditNoteNo",
               table: "Policies",
               nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyCreditNotes");

            migrationBuilder.DropColumn(
                name: "CreditNoteId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CreditNoteNo",
                table: "Policies");

        }
    }
}
