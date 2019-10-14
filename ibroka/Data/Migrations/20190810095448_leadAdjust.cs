using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class leadAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranscationType",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranscationType",
                table: "LeadPolicies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranscationType",
                table: "Policies");

            migrationBuilder.DropColumn(
               name: "TranscationType",
               table: "LeadPolicies");
        }
    }
}
