using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class PermissionEntity : BaseEntity<Permission>
    {
        public string PermissionCode { get; set; }
        public string Name { get; set; }
        public string KeyValue { get; set; }
        public string Level { get; set; }
        public string ParentCode { get; set; }

        public PermissionEntity() { }
        public PermissionEntity(Permission permission) : base(permission)
        {
            Guid = permission.Guid;
            PermissionCode = permission.PermissionCode;
            Name = permission.Name;
            KeyValue = permission.KeyValue;
            Level = permission.Level;
            ParentCode = permission.ParentCode;
            IsActive = permission.IsActive;
            IsDeleted = permission.IsDeleted;
            CreatedDate = permission.CreatedDate;
        }
        public override Permission MapToModel()
        {
            Permission permission = new Permission();
            permission.Guid = Guid.NewGuid();
            permission.PermissionCode = PermissionCode;
            permission.Name = Name;
            permission.KeyValue = KeyValue;
            permission.Level = Level;
            permission.ParentCode = ParentCode;
            permission.IsActive = false;
            permission.IsDeleted = false;
            permission.CreatedDate = CreatedDate;
           
            return permission;
        }

        public override Permission MapToModel(Permission t)
        {
            Permission permission= t;
            permission.Guid = Guid;
            permission.PermissionCode = PermissionCode;
            permission.Name = Name;
            permission.KeyValue = KeyValue;
            permission.Level = Level;
            permission.ParentCode = ParentCode;
            permission.IsActive = IsActive;
            permission.IsDeleted = IsDeleted;
            permission.CreatedDate = CreatedDate;
            return permission;
        }
    }

}