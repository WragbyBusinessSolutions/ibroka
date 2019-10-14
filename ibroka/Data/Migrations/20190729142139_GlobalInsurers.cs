using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ibroka.Data.Migrations
{
    public partial class GlobalInsurers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(

              name: "GlobalInsurers",

              columns: table => new

              {

                  Id = table.Column<Guid>(nullable: false),

                  Name = table.Column<string>(nullable: true),
                  DisplayName = table.Column<string>(nullable: true),

                  PhoneNo = table.Column<string>(nullable: true),

                  Email = table.Column<string>(nullable: true),

                  Address = table.Column<string>(nullable: true),

                  DateModified = table.Column<DateTime>(nullable: true),

                  DateUpdated = table.Column<DateTime>(nullable: true),

                  DateCreated = table.Column<DateTime>(nullable: true),

                  IsDeleted = table.Column<bool>(nullable: true),


              },

              constraints: table =>

              {

                  table.PrimaryKey("PK_GlobalInsurer", x => x.Id);

              });

            migrationBuilder.Sql("insert into GlobalInsurers (Id, Name, DateModified, DateUpdated, DateCreated, IsDeleted) Values('D6566395-56A9-44E6-9624-8ADB751BEE7C', 'HDFC', '2019-07-07', '2019-07-07', '2019-07-07',0)");
            migrationBuilder.Sql("insert into GlobalInsurers(Id, Name, DateModified, DateUpdated, DateCreated, IsDeleted) Values('3411A28F-F5CD-4F9E-AFAA-AA79F84C5885', 'ICICI', '2019-07-07', '2019-07-07', '2019-07-07', 0)");
            migrationBuilder.Sql("insert into GlobalInsurers(Id, Name, DateModified, DateUpdated, DateCreated, IsDeleted) Values('FFFC1F62-E31E-47F3-926F-16312ACFC543', 'IFFCO-TOKIO', '2019-07-07', '2019-07-07', '2019-07-07', 0)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "GlobalInsurers");
        }
    }
}
