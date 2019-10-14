using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class LeadConfigOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadConfigOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadConfigOptions", x => x.Id);
                });

            migrationBuilder.Sql("insert into LeadConfigOptions(id, [Key], [Value]) Values('E601F231-85E0-42AE-8DAB-8669C59F11E0', 'HotLeads', '30')");
            migrationBuilder.Sql("insert into LeadConfigOptions(id, [Key], [Value]) Values('6935AB5A-E307-41E9-BBC5-CE4B0366FA41', 'WarmLeads', '60')");
            migrationBuilder.Sql("insert into LeadConfigOptions(id, [Key], [Value]) Values('1EB24027-C29F-4254-B893-25494BA8EA15', 'CoolLeads', '90')");
            migrationBuilder.Sql("insert into LeadConfigOptions(id, [Key], [Value]) Values('29924C10-50A8-490D-8314-8F39143594FF', 'ColdLeads', '120')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "LeadConfigOptions");
        }

       
    }
}
