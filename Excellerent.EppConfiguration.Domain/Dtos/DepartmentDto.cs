using Excellerent.EppConfiguration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Dtos
{
    public class DepartmentDto
    {
        [Required]
        public string Name { get; set; }

        public DepartmentEntity MapToEntity()
        {
            DepartmentEntity d = new DepartmentEntity();
            d.Name = this.Name;
            return d;
        }
    }
}
