using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ibroka.Models;
using ibroka.Models.HumanResource;
using ibroka.Models.LeadManagement;
using ibroka.Models.AccountBroker;

namespace ibroka.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<EmploymentStatus> EmploymentStatuses { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<PayGrade> PayGrades { get; set; }
    public DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public DbSet<ContactDetail> ContactDetails { get; set; }
    public DbSet<Dependant> Dependants { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Salary> Salaries { get; set; }


    public DbSet<Branch> Branches { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Leave> Leaves { get; set; }

    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<LeaveConfiguration> LeaveConfigurations { get; set; }
    public DbSet<OrganisationAsset> OrganisationAssets { get; set; }
    public DbSet<InstitutionQualification> InstitutionQualifications { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }

    public DbSet<Deduction> Deductions { get; set; }
    public DbSet<Addition> Additions { get; set; }
    public DbSet<SalaryAddition> SalaryAdditions { get; set; }
    public DbSet<SalaryDeduction> SalaryDeductions { get; set; }

    public DbSet<Payroll> Payrolls { get; set; }

    public DbSet<Appraisal> Appraisals { get; set; }
    
    public DbSet<AppraisalCategory> AppraisalCategories { get; set; }
    public DbSet<AppraisalKPI> AppraisalKPIs { get; set; }
    public DbSet<AppraisalTemplate> AppraisalTemplates { get; set; }
    //public DbSet<AppraisalAssignedTemplate> AppraisalAssignedTemplates { get; set; }
    public DbSet<AppraisalAssignedTemplate> AppraisalAssignedTemplates { get; set; }



    public DbSet<AppraisalTemplateCategory> AppraisalTemplateCategories { get; set; }

    public DbSet<AssignedSubordinate> AssignedSubordinates { get; set; }
    public DbSet<AssignedSupervisor> AssignedSupervisors { get; set; }

        public DbSet<PolicyClassMaster> PolicyClassMaster { get; set; }
        public DbSet<LeadConfigOption> LeadConfigOptions { get; set; }
        public DbSet<GlobalInsurer> GlobalInsurers { get; set; }
        public DbSet<InsurerMaster> InsurerMasters { get; set; }

        public DbSet<BussinessOperationConfiguration> BussinessOperationConfigurations { get; set; }
        public DbSet<LeadPolicyField> LeadPolicyFields { get; set; }
        public DbSet<LeadPolicyData> LeadPolicyData { get; set; }
        public DbSet<LeadPolicy> LeadPolicies { get; set; }
        public DbSet<LeadPolicyDebitNote> LeadPolicyDebitNotes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CorporateDirector> CorporateDirectors { get; set; }
        public DbSet<LeadPaymentDetail> LeadPaymentDetails { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyCreditNote> PolicyCreditNotes { get; set; }
        public DbSet<Endorsement> Endorsements { get; set; }
        public DbSet<LeadEndorsement> LeadEndorsements { get; set; }
        public DbSet<EndorsementDebitNote> EndorsementDebitNotes { get; set; }
        public DbSet<LeadEndorsementPaymentDetail> LeadEndorsementPaymentDetails { get; set; }
        public DbSet<LeadDocument> LeadDocuments { get; set; }
        public DbSet<EndorsementCreditNote> EndorsementCreditNotes { get; set; }



    public DbSet<Imprest> Imprests { get; set; }
    public DbSet<IncomeType> IncomeTypes { get; set; }
    public DbSet<ExpenseType> ExpenseTypes { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    }
}
