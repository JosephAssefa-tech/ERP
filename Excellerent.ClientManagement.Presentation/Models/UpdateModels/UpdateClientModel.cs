using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Models.UpdateModels
{
    public class UpdateClientModel
    {
        public Guid Guid { get; set; }
        public Guid SalesPersonGuid { get; set; }
        public Guid ClientStatusGuid { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public List<UpdateClientContactModel> ClientContacts { get; set; }
        public List<UpdateCompanyContactModel> CompanyContacts { get; set; }
        public List<UpdateOpperatingAddressModel> OperatingAddress { get; set; }
        public List<UpdateBillingAddressModel> BillingAddress { get; set; }
    }


    public static class ClientDetailsEntityMapper
    {
        public static ClientDetailsEntity MappToEntity(this UpdateClientModel model)
        {
            ClientDetailsEntity entity = new ClientDetailsEntity();
            entity.Guid = model.Guid;
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