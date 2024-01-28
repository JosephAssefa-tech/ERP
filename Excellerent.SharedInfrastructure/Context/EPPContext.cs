
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Models;

using Excellerent.ResourceManagement.Domain.Models;

using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Usermanagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Employee = Excellerent.ResourceManagement.Domain.Models.Employee;
using Excellerent.EppConfiguration.Domain.Model;
using System.Data.SqlClient;
using Excellerent.ReportManagement.Core.Entities.Models;

namespace Excellerent.SharedInfrastructure.Context
{
    public partial class EPPContext : DbContext
    {
        public EPPContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantAreaOfInterest> ApplicantAreaOfInterest { get; set; }
        public DbSet<LUPositionToApply> PositionToApply { get; set; }
        public DbSet<LUProficiencyLevel> ProficiencyLevel { get; set; }

        public DbSet<LUSkillPositionAssociation> SkillPositionAssociation { get; set; }

        public DbSet<JobRequirment> JobRequirment { get; set; }
        public DbSet<LUSkillSet> SkillSet { get; set; }
        
        public DbSet<LUPositionSkillSet> LUPositionSkillSet { get; set; }

        public DbSet<LUFieldOfStudy> FieldOfStudie { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeEntry> TimeEntry { get; set; }
        public DbSet<TmpTimeEntry> TmpTimeEntries { get; set; }
        public DbSet<Timesheet.Domain.Models.Configuration> TimeSheetConfigurations { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Education> Educations { get; set; }
        public DbSet<LUEducationProgram> EducationProgrammes { get; set; }
        public DbSet<TimesheetApproval> TimesheetAprovals { get; set; }

        public DbSet<AssignResourcEntity> AssignResources { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientDetails> ClientDetails { get; set; }
        public DbSet<Project> Project { get; set; }

        public DbSet<EmergencyContactsModel> emergencycontacts { get; set; }

        public DbSet<EmployeeOrganization> EmployeeOrganizations { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<DutyBranch> DutyBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<FamilyDetails> FamilyDetails { get; set; }
        public DbSet<EmergencyAddress> EmergencyAddresses { get; set; }
        public DbSet<PersonalAddress> PersonalAddresses { get; set; }
        public DbSet<EmergencyContactsModel> emergencycontact { get; set; }
        public DbSet<DeviceDetails> DeviceDetails { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<OperatingAddress> OperatingAddresses { get; set; }
        public DbSet<ClientStatus> ClientStatus { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<User> Users { get; set; }       
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GroupSet> GroupSets { get; set; }
        public DbSet<GroupSetPermission> GroupSetPermissions { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        public DbSet<EmployeeBrief> EmployeeBriefs { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<ProjectResourceTimesheet> ProjectResourceTimesheets { get; set; }
        public DbSet<TimesheetResourceNotification> TimesheetResourceNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<TimesheetApproval>().HasKey(table => new { table.TimesheetId, table.ProjectId });

            modelBuilder.Entity<EmployeeBrief>().HasNoKey();
            modelBuilder.Entity<ProjectResource>().HasNoKey();
            modelBuilder.Entity<ProjectResourceTimesheet>().HasNoKey();
            modelBuilder.Entity<TimesheetResourceNotification>().HasNoKey();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedbyUserGuid = Guid.Empty;
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
