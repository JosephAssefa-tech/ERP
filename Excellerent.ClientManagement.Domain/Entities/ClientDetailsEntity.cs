using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class ClientDetailsEntity : BaseEntity<ClientDetails>
    {
        public string SalesPersonName { get; set; }
        public EmployeeDTO SalesPerson { get; set; }
        public string OperatingAddressCountry { get; set; }
        public string ClientStatusName { get; set; }
        public Guid SalesPersonGuid { get; set; }
        public string ClientName { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public Guid ClientStatusGuid { get; set; }
        public string Description { get; set; }
        
        public List<ClientContactEntity> ClientContacts { get; set; }
        

        public List<CompanyContactEntity> CompanyContacts { get; set; }
        

        public List<OperatingAddressEntity> OperatingAddress { get; set; }
        

        public List<BillingAddressEntity> BillingAddress { get; set; }

        public ClientDetailsEntity()
        {
            this.IsActive = true;
            Guid = Guid.NewGuid();
        }

        public ClientDetailsEntity(ClientDetails clientDetails) 
        {
            SalesPersonGuid = clientDetails.SalesPersonGuid;
            Guid = clientDetails.Guid;
            ClientName = clientDetails.ClientName;
            ClientStatusGuid = clientDetails.ClientStatusGuid;
            Description = clientDetails.Description;

            ClientStatus = clientDetails.ClientStatus;
            ClientContacts = clientDetails.ClientContacts != null ? clientDetails.ClientContacts.Select(x => new ClientContactEntity(x)).ToList() : null;
            CompanyContacts = clientDetails.CompanyContacts != null ? clientDetails.CompanyContacts.Select(y => new CompanyContactEntity(y)).ToList() : null;
            BillingAddress = clientDetails.BillingAddress != null ? clientDetails.BillingAddress.Select(z => new BillingAddressEntity(z)).ToList() : null;
            OperatingAddress = clientDetails.OperatingAddress != null ? clientDetails.OperatingAddress.Select(x => new OperatingAddressEntity(x)).ToList() : null;
        }

        public override ClientDetails MapToModel()
        {
            ClientDetails clientDetails = new ClientDetails();
            clientDetails.SalesPersonGuid = SalesPersonGuid;
            clientDetails.ClientName = ClientName;
            clientDetails.ClientStatusGuid = ClientStatusGuid;
            clientDetails.Description = Description;
            clientDetails.ClientContacts = ClientContacts.Select(x => x.MapToModel()).ToList();
            clientDetails.CompanyContacts = CompanyContacts.Select(x => x.MapToModel()).ToList();
            clientDetails.BillingAddress = BillingAddress.Select(x => x.MapToModel()).ToList();
            clientDetails.OperatingAddress = OperatingAddress.Select(x => x.MapToModel()).ToList();

            return clientDetails;
        }


        public override ClientDetails MapToModel(ClientDetails t)
        {
            ClientDetails clientDetails = t;
            clientDetails.SalesPersonGuid = SalesPersonGuid;
            clientDetails.ClientName = ClientName;
            clientDetails.ClientStatusGuid = ClientStatusGuid;
            clientDetails.Description = Description;
            clientDetails.ClientContacts = ClientContacts.Select(x => x.MapToModel(t.ClientContacts.FirstOrDefault(y=>y.Guid==x.Guid))).ToList();
            clientDetails.CompanyContacts = CompanyContacts.Select(x => x.MapToModel(t.CompanyContacts.FirstOrDefault(y => y.Guid == x.Guid))).ToList();
            clientDetails.BillingAddress = BillingAddress.Select(x => x.MapToModel(t.BillingAddress.FirstOrDefault(y => y.Guid == x.Guid))).ToList();
            clientDetails.OperatingAddress = OperatingAddress.Select(x => x.MapToModel(t.OperatingAddress.FirstOrDefault(y => y.Guid == x.Guid))).ToList();
            return clientDetails;
        }
    }
}