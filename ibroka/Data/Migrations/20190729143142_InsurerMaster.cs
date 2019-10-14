using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class InsurerMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(

              name: "InsurerMasters",

              columns: table => new

              {

                  Id = table.Column<Guid>(nullable: false),

                  DateModified = table.Column<DateTime>(nullable: true),

                  DateUpdated = table.Column<DateTime>(nullable: true),

                  DateCreated = table.Column<DateTime>(nullable: true),

                  IsDeleted = table.Column<bool>(nullable: true),

                  OrganisationId = table.Column<Guid>(nullable: false),
                  GlobalInsurerId = table.Column<Guid>(nullable: false),


              },

              constraints: table =>

              {

                  table.PrimaryKey("PK_InsurerMaster", x => x.Id);

              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "InsurerMaster");

        }
    }
}
