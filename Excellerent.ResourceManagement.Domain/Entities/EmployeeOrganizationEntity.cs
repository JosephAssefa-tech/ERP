using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using Excellerent.EppConfiguration.Domain.Model;
using System;


namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class EmployeeOrganizationEntity : BaseEntity<EmployeeOrganization>
    { 
        public Country Country { get; set; }

        public Guid CountryId { get; set; }

        public Guid DutyBranchId { get; set; }

        public DutyBranch DutyBranch { get; set; }

        public Guid EmployeeId { get; set; }

        public string CompaynEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string EmploymentType { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }

        public Guid? ReportingManager { get; set; }

        public Guid JobTitleId { get; set; }

        public Role Role { get; set; }

        public string Status { get; set; }

        public EmployeeOrganizationEntity()
        {

        }

        public EmployeeOrganizationEntity(EmployeeOrganization employeeOrganization) : base(employeeOrganization)
        {
            Guid = employeeOrganization.Guid;
            //EmployeeId = employeeOrganization.EmployeeId;
            Country = employeeOrganization.Country;
            DutyBranch = employeeOrganization.DutyBranch; 
            CompaynEmail = employeeOrganization.CompaynEmail;
            ReportingManager = employeeOrganization.ReportingManager;
            DepartmentId = employeeOrganization.DepartmentId;
            JobTitleId = employeeOrganization.JobTitleId;
            JoiningDate = employeeOrganization.JoiningDate;
            TerminationDate = employeeOrganization.TerminationDate;
            Status = employeeOrganization.Status;
            CreatedDate = employeeOrganization.CreatedDate;
            CreatedbyUserGuid = employeeOrganization.CreatedbyUserGuid;
            IsActive = employeeOrganization.IsActive;
            IsDeleted = employeeOrganization.IsDeleted;
        }

        public override EmployeeOrganization MapToModel()
        {
            EmployeeOrganization employeeOrganization = new EmployeeOrganization();
            employeeOrganization.Guid = Guid;
            //employeeOrganization.EmployeeId = EmployeeId;
            employeeOrganization.Country = Country;
            employeeOrganization.DutyBranch = DutyBranch;
            employeeOrganization.CompaynEmail = CompaynEmail;
            employeeOrganization.ReportingManager = ReportingManager;
            employeeOrganization.DepartmentId = DepartmentId;
            employeeOrganization.JobTitleId = JobTitleId;
            employeeOrganization.JoiningDate = JoiningDate;
            employeeOrganization.TerminationDate = TerminationDate;
            employeeOrganization.Status = Status;
            //employeeOrganization.CreatedDate = CreatedDate;
            //employeeOrganization.CreatedbyUserGuid = CreatedbyUserGuid;
            //employeeOrganization.IsActive = IsActive;
            //employeeOrganization.IsDeleted = IsDeleted;

            return employeeOrganization;

        }

        public override EmployeeOrganization MapToModel(EmployeeOrganization t)
        {
            EmployeeOrganization employeeOrganization = t;
            employeeOrganization.Guid = Guid;
            //employeeOrganization.EmployeeId = EmployeeId;
            employeeOrganization.Country = Country;
            employeeOrganization.DutyBranch = DutyBranch;
            employeeOrganization.CompaynEmail = CompaynEmail;
            employeeOrganization.ReportingManager = ReportingManager;
            employeeOrganization.DepartmentId = DepartmentId;
            employeeOrganization.JobTitleId = JobTitleId;
            employeeOrganization.JoiningDate = JoiningDate;
            employeeOrganization.TerminationDate = TerminationDate;
            employeeOrganization.Status = Status;
            employeeOrganization.CreatedDate = CreatedDate;
            employeeOrganization.CreatedbyUserGuid = CreatedbyUserGuid;
            employeeOrganization.IsActive = IsActive;
            employeeOrganization.IsDeleted = IsDeleted;

            return employeeOrganization;
        }
    }
}
