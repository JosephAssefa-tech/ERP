using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;

namespace Excellerent.ClientManagement.Domain.DTOs
{
    public class ClientDTO : BaseAuditModel
    {
        public string ClientName { get; set; }
        public IEnumerable<object> Projects { get; set; }
        public ClientDTO(ClientDetailsEntity clientDetailsEntity)
        {
            Guid = clientDetailsEntity.Guid;
            IsActive = clientDetailsEntity.IsActive;
            IsDeleted = clientDetailsEntity.IsDeleted;
            CreatedDate = clientDetailsEntity.CreatedDate;
            CreatedbyUserGuid = clientDetailsEntity.CreatedbyUserGuid;
            ClientName = clientDetailsEntity.ClientName;
        }
        public ClientDTO(ClientDetailsEntity clientDetailsEntity, IEnumerable<object> projects)
        {
            Guid = clientDetailsEntity.Guid;
            IsActive = clientDetailsEntity.IsActive;
            IsDeleted = clientDetailsEntity.IsDeleted;
            CreatedDate = clientDetailsEntity.CreatedDate;
            CreatedbyUserGuid = clientDetailsEntity.CreatedbyUserGuid;
            ClientName = clientDetailsEntity.ClientName;
            Projects = projects;
        }
    }
}
