using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.EppConfiguration.Presentation.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Mapping
{
    public static class RoleMapping
    {
        public static RoleDto MapToDto(this Role role)
        {
            RoleDto dto = new RoleDto();
            dto.Guid = role.Guid;
            dto.Name = role.Name;
            dto.DepartmentGuid = role.DepartmentGuid;
            dto.DepartmentName = role.Department.Name;
            return dto;

        }
    }
}
