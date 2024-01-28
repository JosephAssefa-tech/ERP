using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class CompanyContact : BaseAuditModel
    {
        public Guid EmployeeGuid { get; set; }
        public Guid ClientDetailsGuid { get; set; }

    }
}