using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class insurerMasterAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebUrl",
                table: "GlobalInsurers",
                nullable: true);

            migrationBuilder.AddColumn<Decimal>(
                name: "Commission",
                table: "GlobalInsurers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebUrl",
                table: "InsurerMasters",
                nullable: true);

            migrationBuilder.AddColumn<Decimal>(
                name: "Commission",
                table: "InsurerMasters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InsurerMasters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GlobalInsurers",
                nullable: true);

            //

            migrationBuilder.AddColumn<string>(
               name: "Name",
               table: "InsurerMasters",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "DisplayName",
               table: "InsurerMasters",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "PhoneNo",
               table: "InsurerMasters",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "Email",
               table: "InsurerMasters",
               nullable: true);

            migrationBuilder.AddColumn<string>(
              name: "Address",
              table: "InsurerMasters",
              nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                 name: "WebUrl",
                 table: "GlobalInsurers");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "GlobalInsurers");

            migrationBuilder.DropColumn(
                name: "WebUrl",
                table: "InsurerMasters");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "InsurerMasters");

            //

            migrationBuilder.DropColumn(
               name: "Name",
               table: "InsurerMasters");

            migrationBuilder.DropColumn(
               name: "DisplayName",
               table: "InsurerMasters");

            migrationBuilder.DropColumn(
               name: "PhoneNo",
               table: "InsurerMasters");

            migrationBuilder.DropColumn(
               name: "Email",
               table: "InsurerMasters");

            migrationBuilder.DropColumn(
              name: "Address",
              table: "InsurerMasters");

            migrationBuilder.DropColumn(
               name: "Description",
               table: "InsurerMasters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GlobalInsurers");
        }
    }
}
