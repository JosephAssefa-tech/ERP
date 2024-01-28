using Excellerent.SharedModules.Seed;
using System;
namespace Excellerent.ResourceManagement.Domain.Models
{
    public class FamilyDetails : BaseAuditModel
    {
        public Guid RelationshipId{ get; set; }
        public Guid EmployeeId { get; set; }
        public Relationship Relationship { get; set; }
        public String FullName { get; set; }
        public String Gender { get; set; }
        public DateTime? DoB { get; set; }
        public String Remark { get; set; }
    }
}