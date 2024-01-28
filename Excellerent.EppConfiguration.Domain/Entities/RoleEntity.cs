using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.EppConfiguration.Presentation.Dtos;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.EppConfiguration.Domain.Entities
{
    public class RoleEntity : BaseEntity<Role>
    {
        public string Name { get; set; }
        public Guid DepartmentGuid { get; set; }
        public DepartmentEntity DepartmentEntity { get; set; }

        public RoleEntity() { }

        public RoleEntity(Role r) : base(r)
        {
            this.Name = r.Name;
            this.DepartmentGuid = r.DepartmentGuid;
            this.DepartmentEntity = r.Department != null ? new DepartmentEntity(r.Department) : null;
        }
        public override Role MapToModel()
        {
            Role r = new Role();
            r.Guid = Guid;
            r.Name = Name;
            r.DepartmentGuid = DepartmentGuid;
            // r.Department = DepartmentEntity.MapToModel();

            return r;
        }

        public override Role MapToModel(Role t)
        {
            Role d = new Role();
            d.Guid = t.Guid;
            d.Name = t.Name;
            d.DepartmentGuid = t.DepartmentGuid;
            d.Department = t.Department;
            return d;
        }

        public RoleDto MapToDto()
        {
            RoleDto dto = new RoleDto();
            dto.Guid = Guid;
            dto.Name = Name;
            dto.DepartmentGuid = DepartmentGuid;
            dto.DepartmentName = this.DepartmentEntity.Name;
            return dto;
        }
    }
}
