using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.ResourceManagement.Infrastructure.Dtos
{
    public class EmergencyContactDto
    {


        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string Relationship { get; set; }
        public Guid EmployeeGuid { get; set; }
        public List<EmergencyAddress> Address { get; set; }

        public EmergencyContactDto()
        {

        }

        public EmergencyContactDto(EmergencyContactDto dto)
        {

        }

    }
}
