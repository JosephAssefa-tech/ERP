using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class CompanyContactEntity : BaseEntity<CompanyContact>
    {
        public Guid EmployeeGuid { get; set; }
        public EmployeeDTO Employee { get; set; }

        public CompanyContactEntity()
        {
            this.IsActive = true;
        }

        public CompanyContactEntity(CompanyContact comany) : base(comany)
        {
            EmployeeGuid = comany.EmployeeGuid;
            Guid = comany.Guid;
        }

        public override CompanyContact MapToModel()
        {
            CompanyContact companyContact = new CompanyContact();

            companyContact.EmployeeGuid = EmployeeGuid;
            return companyContact;
        }

        public override CompanyContact MapToModel(CompanyContact t)
        {
            CompanyContact company = t;
            company.EmployeeGuid = EmployeeGuid;

            return company;
        }
    }
}