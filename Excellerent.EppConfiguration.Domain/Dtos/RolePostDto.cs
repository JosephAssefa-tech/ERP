using Excellerent.EppConfiguration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Dtos
{
    public class RolePostDto
    {
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid DepartmentGuid { get; set; }

        public RoleEntity MapToEntity()
        {
            RoleEntity e = new RoleEntity();
            e.Name = this.Name;
            e.DepartmentGuid = this.DepartmentGuid;
            return e;
        }
    }
}
