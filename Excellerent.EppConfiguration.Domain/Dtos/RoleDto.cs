using Excellerent.EppConfiguration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Dtos
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string DepartmentName { get; set; }

        public RoleEntity MapToEntity()
        {
            RoleEntity e = new RoleEntity();
            e.Name = this.Name;
            e.DepartmentGuid = this.DepartmentGuid;
            // e.DepartmentEntity = this.DepartmentEntity;
            return e;
        }
    }
}
