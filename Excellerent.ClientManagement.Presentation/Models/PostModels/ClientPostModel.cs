using Excellerent.ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class ClientPostModel
    {
        public Guid SalesPersonGuid { get; set; }
        public Guid ClientStatusGuid { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public List<ClientContactPostModel> ClientContacts { get; set; }
        public List<ComapanyContactPostModel> CompanyContacts { get; set; }
        public List<OperatingAddressPostModel> OperatingAddress { get; set; }
        public List<BillingAddressPostModel> BillingAddress { get; set; }
    }


    public static class ClientDetailsEntityMapper
    {
        public static ClientDetailsEntity MappToEntity(this ClientPostModel model)
        {
            ClientDetailsEntity entity = new ClientDetailsEntity();
            entity.SalesPersonGuid = model.SalesPersonGuid;
            entity.ClientStatusGuid = model.ClientStatusGuid;
            entity.ClientName = model.ClientName;
            entity.Description = model.Description;
            entity.ClientContacts = model.ClientContacts.Select(x => x.MappToEntity()).ToList();
            entity.CompanyContacts = model.CompanyContacts.Select(x => x.MappToEntity()).ToList(); ;
            entity.BillingAddress = model.BillingAddress.Select(x => x.MappToEntity()).ToList();
            entity.OperatingAddress = model.OperatingAddress.Select(x => x.MappToEntity()).ToList();
            return entity;
        }
    }
}