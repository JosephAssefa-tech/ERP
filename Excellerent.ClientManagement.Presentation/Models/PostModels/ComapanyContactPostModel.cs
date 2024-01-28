using Excellerent.ClientManagement.Domain.Entities;
using System;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class ComapanyContactPostModel
    {
        public Guid ContactPersonGuid { get; set; }
    }

    public static class ComapanyContactEntityMapper
    {
        public static CompanyContactEntity MappToEntity(this ComapanyContactPostModel model)
        {
            CompanyContactEntity entity = new CompanyContactEntity();
            entity.EmployeeGuid = model.ContactPersonGuid;

            return entity;
        }
    }
}