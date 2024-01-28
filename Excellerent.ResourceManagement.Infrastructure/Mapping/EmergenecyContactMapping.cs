using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ResourceManagement.Infrastructure.Mapping
{
    public static class EmergenecyContactMapping
    {

        public static EmergencyContactDto ToDto(this EmergencyContactsModel entity)
        {
            return new EmergencyContactDto
            {
                FirstName = entity.FirstName,
                FatherName = entity.FatherName,
                Relationship = entity.Relationship,

                //Address = entity.Address?.Select(x => new EmergencyAddress()).ToList(),

            };
        }
        public static EmergencyContactsModel ToEntity(this EmergencyContactDto dto)
        {
            return new EmergencyContactsModel
            {
                FirstName = dto.FirstName,
                FatherName = dto.FatherName,
                Relationship = dto.Relationship,

                //Address = dto.Address?.Select(model => new EmergencyAddress()),

            };
        }
    }
}
