using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class BussinessOperationConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(

                name: "BussinessOperationConfigurations",

                columns: table => new

                {

                    Id = table.Column<Guid>(nullable: false),

                    Key = table.Column<string>(nullable: false),

                    Value = table.Column<string>(nullable: false),

                    DateCreated = table.Column<DateTime>(nullable: true),

                    DateModified = table.Column<DateTime>(nullable: true),

                    DateUpdated = table.Column<DateTime>(nullable: true),

                    IsDeleted = table.Column<bool>(nullable: true),

                    OrganisationId = table.Column<Guid>(nullable: false),

                    CreatedBy = table.Column<Guid>(nullable: true),

                    ModifiedBy = table.Column<Guid>(nullable: true)

                },

                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinessOperationConfigurations", x => x.Id);

                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BussinessOperationConfigurations");

        }
    }
}
