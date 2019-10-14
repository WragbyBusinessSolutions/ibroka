using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class imprest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imprests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Particular = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imprests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadPolicies_ClientId",
                table: "LeadPolicies",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadPolicies_Clients_ClientId",
                table: "LeadPolicies",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadPolicies_Clients_ClientId",
                table: "LeadPolicies");

            migrationBuilder.DropTable(
                name: "Imprests");

            migrationBuilder.DropIndex(
                name: "IX_LeadPolicies_ClientId",
                table: "LeadPolicies");
        }
    }
}
