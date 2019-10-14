using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class policyAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiptImgUrl",
                table: "Policies",
                nullable: true
                );

            migrationBuilder.AddColumn<int>(
                name: "EndorsementCount",
                table: "Policies",
                nullable: true
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptImgUrl",
                table: "Policies");

            migrationBuilder.DropColumn(
               name: "EndorsementCount",
               table: "Policies"
               );
        }
    }
}
