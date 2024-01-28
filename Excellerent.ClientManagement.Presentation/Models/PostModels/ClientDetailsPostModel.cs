using Excellerent.ClientManagement.Domain.Entities;
using System;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class ClientDetailsPostModel
    {
        public Guid SalesPersonGuid { get; set; }
        public string ClientName { get; set; }
        public Guid ClientStatusGuid { get; set; }
        public string Description { get; set; }
    }

    public static class MappClientEntity
    {
        public static ClientDetailsEntity MappToEntity(this ClientDetailsPostModel model)
        {
            ClientDetailsEntity clientDetailsEntity = new ClientDetailsEntity();

            clientDetailsEntity.SalesPersonGuid = model.SalesPersonGuid;
            clientDetailsEntity.ClientName = model.ClientName;
            if (string.IsNullOrEmpty(model.ClientStatusGuid.ToString()))
            {
                clientDetailsEntity.ClientStatusGuid = Guid.Empty;
            }
            if (!string.IsNullOrEmpty(model.ClientStatusGuid.ToString()))
            {
                clientDetailsEntity.ClientStatusGuid = model.ClientStatusGuid;
            }
            clientDetailsEntity.Description = model.Description;

            return clientDetailsEntity;
        }
    }
}