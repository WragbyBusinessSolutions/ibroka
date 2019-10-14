using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace E4S.Data.Migrations
{
    public partial class LeadPolicyField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadPolicyFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PolicyClassId = table.Column<Guid>(nullable: true),
                    FieldName = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    FieldType = table.Column<string>(nullable: false),
                    FieldData = table.Column<string>(nullable: true),

                    DateUpdated = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadPolicyFields", x => x.Id);
                });

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)  Values('433F1501-75AC-4B94-AC70-0AE80239C0DA', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'MakeOfVehicle', 'text', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Make Of Vehicle')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)   Values('E43FE1FC-9B69-4B26-A60E-192D871B626C', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'ModelOfVehicle', 'text', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Model Of Vehicle')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)                                   Values('B5DA5C72-E23F-4E2E-ACBC-9A049FBBE7EC', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'YearOfMake', 'int', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Year Of Make')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)        Values('825316C4-682B-43AD-81CE-C45B2AF1BE1C', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'CubicCapacity', 'text', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Cubic Capacity')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)        Values('F0FED55E-3976-4A3B-9FFA-3C5916BC9517', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'RegistrationNo', 'text', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Registration No')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)        Values('EF074597-2317-4F54-9FCF-6F6E09EFF99D', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'ValueInsured', 'text', '', '2019-07-27', '2019-07-27', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 'A9BDAF6F-FFEE-4317-AD35-E42633147E4D', 0, 'Value Insured')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('51EEF8FA-7635-4C16-925F-F82B9B8C535A', 'C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5', 'RiskType', 'choice', '{  \"RiskType\": \"[{Title:''Private''},{Title:''Commercial''}]\"   }' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Risk Type')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('75D13929-AEE5-45E6-BD17-82E17B5078C0','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'MakeOfVehicle','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Make Of Vehicle')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('0401F564-74F9-4565-8F4F-763BDAEC9992','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'ModelOfVehicle','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Model Of Vehicle')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)     Values('AB23D1DB-E0D6-47AB-9305-4057EA79CAB4','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'YearOfMake','int','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Year Of Make')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('E1FC0203-B97A-47D1-A0E4-6CF97F6AD5CA','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'CubicCapacity','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Cubic Capacity')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('BD8ED6CF-BB14-407D-BD6F-CF677F59EDCF','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'RegistrationNo','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Registration No')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('393AE007-6240-46D2-9044-A398DAC33AC0','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'ValueInsured','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Value Insured')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)    Values('49ED366B-5C89-469A-9586-602149956E60','FCF9DDF7-BF80-4503-9C5E-CECF428491EC', 'RiskType','choice','{  \"RiskType\": \"[{Title:''Private''},{Title:''Commercial''}]\"}' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Risk Type')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)       Values('EB20D8CD-2573-4A25-B099-324547357BAE','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'NameOfAssured','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Name Of Assured')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)     Values('49CC9534-72AC-4A20-AF53-7A83C2782525','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'DOB','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Date Of Birth')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)      Values('6DC1BD17-8B6C-4669-A7E3-7A80C844772D','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'Sex','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Sex')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)        Values('ACB730F8-9F23-4A1A-9F08-3A68E47CDEEC','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'Salary','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Salary')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)             Values('DCF8DC3A-6D3E-4A0A-99DD-C74EA1093078','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'SumAssessed','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Sum Assessed')");



            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)          Values('C38B7F93-2806-414F-BE0D-36A3FDC29174','8ED74480-CA28-418A-ADD6-FE25E8E15B86', 'NextOfKin','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Next Of Kin')");




            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)       Values('67414BC8-4C18-49BD-B697-A68180010E22','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'NameOfAssured','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Name Of Assured')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)          Values('11F2E058-AAD8-4E88-BB15-418FA95C4963','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'DOB','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Date Of Birth')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)                  Values('450D7823-3A6D-4F4C-8E01-7C17F3493C3B','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'Sex','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Sex')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)           Values('7DCBEED8-7DB9-411E-9A79-477BC69E2156','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'Salary','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Salary') ");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)         Values('484E35F1-23AD-4F08-A388-9E3102BBBF08','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'SumAssessed','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Sum Assessed')");



            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)               Values('997FF2EC-CF06-41BA-8E06-F5FF38F61139','D2CFA791-27FC-4CEF-970E-9251FC92D120', 'NextOfKin','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Next Of Kin')");



            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)                Values('4F97D045-AF82-4A1C-8147-0ACACE8CEBAD','E56249E3-E821-4969-80C4-B97BD4798CC2', 'NatureOfContract','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Nature Of Contract')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)        Values('35E02A99-2F47-46D3-96E7-59DDDE3FBF34','E56249E3-E821-4969-80C4-B97BD4798CC2', 'ProjectName','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Project Name')");


            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)              Values('8231C64D-A4A7-4D71-92E6-9409DB7E6EC2','E56249E3-E821-4969-80C4-B97BD4798CC2', 'PremiumRates','text','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Premium Rates')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName)                 Values('A5551E79-CC73-4F39-9A73-19C70EA9172D','E56249E3-E821-4969-80C4-B97BD4798CC2', 'ContractAmount','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0,'Contract Amount')");

            migrationBuilder.Sql("insert into LeadPolicyFields(Id, PolicyClassId, FieldName, FieldType, FieldData, DateUpdated, DateCreated, CreatedBy, ModifiedBy, IsDeleted, DisplayName) Values('30603DB8-A979-4332-898E-41730EAD2FFE','E56249E3-E821-4969-80C4-B97BD4798CC2', 'BondAmount','decimal','' ,'2019-07-27','2019-07-27','A9BDAF6F-FFEE-4317-AD35-E42633147E4D','A9BDAF6F-FFEE-4317-AD35-E42633147E4D',0, 'Bond Amount')");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadPolicyFields");
        }
    }
}
