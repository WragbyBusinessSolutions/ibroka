using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class EndorsementCreditNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "EndorsementCreditNotes",
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
                    EndorsementId = table.Column<Guid>(nullable: false),
                    CreditNote_No = table.Column<Int64>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),



                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndorsementCreditNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndorsementCreditNotes_Endorsements_EndorsementId",
                        column: x => x.EndorsementId,
                        principalTable: "Endorsements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('EndorsementCreditNotes', RESEED, 100000000001);");

            migrationBuilder.AddColumn<Guid>(
                name: "CreditNoteId",
                table: "Endorsements",
                nullable: true);

            migrationBuilder.AddColumn<Int64>(
               name: "CreditNoteNo",
               table: "Endorsements",
               nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndorsementCreditNotes");

            migrationBuilder.DropColumn(
                name: "CreditNoteId",
                table: "Endorsements");

            migrationBuilder.DropColumn(
                name: "CreditNoteNo",
                table: "Endorsements");

        }
    }
}
