using System;
using System.ComponentModel.DataAnnotations.Schema;
using Excellerent.SharedModules.Seed;
using System.ComponentModel.DataAnnotations;
using Excellerent.EppConfiguration.Domain.Model;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class EmployeeOrganization : BaseAuditModel
    {
        public virtual Country Country { get; set; }

        [Required]
        [ForeignKey("Country")]
        public Guid CountryId { get; set; }

        [Required]
        [ForeignKey("DutyBranch")]
        public Guid DutyBranchId { get; set; }

        public virtual DutyBranch DutyBranch { get; set; }


        [ForeignKey("Employees")]
        public Guid EmployeeId { get; set; }

        [Required]
        [EmailAddress]
        public string CompaynEmail { get; set; }


        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        [Required]
        public string EmploymentType { get; set; }

        public virtual Department Department { get; set; }

        [Required]
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        [Required]
        public Guid? ReportingManager { get; set; }

        public virtual Role Role { get; set; }

        [Required]
        [ForeignKey("Role")]
        public Guid JobTitleId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
