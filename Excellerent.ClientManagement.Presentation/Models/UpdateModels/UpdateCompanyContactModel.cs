using Excellerent.ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Models.UpdateModels
{
    public class UpdateCompanyContactModel
    {
        public Guid Guid { get; set; }
        public Guid ContactPersonGuid { get; set; }
    }

    public static class ComapanyContactEntityMapper
    {
        public static CompanyContactEntity MappToEntity(this UpdateCompanyContactModel model)
        {
            CompanyContactEntity entity = new CompanyContactEntity();
            entity.Guid = model.Guid;
            entity.EmployeeGuid = model.ContactPersonGuid;
            return entity;
        }
    }
}