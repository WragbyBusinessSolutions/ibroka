using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class PolicyClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PolicyClassMaster",
               columns: table => new
               {
                   Id = table.Column<Guid>(nullable: false),
                   Name = table.Column<string>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_PolicyClassMaster", x => x.Id);
               });

            migrationBuilder.Sql("INSERT INTO PolicyClassMaster (Id, Name) VALUES('C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'Third party motor policy')");
            migrationBuilder.Sql("INSERT INTO PolicyClassMaster(Id, Name) VALUES('FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'Comprehensive motor policy')");
            migrationBuilder.Sql("INSERT INTO PolicyClassMaster(Id, Name) VALUES('8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'Life policy')");
            migrationBuilder.Sql("INSERT INTO PolicyClassMaster(Id, Name) VALUES('D2CFA791-27FC-4CEF-970E-9251FC92D120', 'Group life policy')");
            migrationBuilder.Sql("INSERT INTO PolicyClassMaster(Id, Name) VALUES('E56249E3-E821-4969-80C4-B97BD4798CC2', 'Bond policy')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "ClientMaster");
        }
    }
}
