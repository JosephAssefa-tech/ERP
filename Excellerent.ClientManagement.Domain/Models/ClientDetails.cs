using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class ClientDetails : BaseAuditModel
    {
        public Guid SalesPersonGuid { get; set; }
        public string ClientName { get; set; }
        public Guid ClientStatusGuid { get; set; }
        public virtual ClientStatus ClientStatus { get; set; }
        public string Description { get; set; }

        public List<ClientContact> ClientContacts { get; set; }
        public List<CompanyContact> CompanyContacts { get; set; }
        public List<OperatingAddress> OperatingAddress { get; set; }
        public List<BillingAddress> BillingAddress { get; set; }
    }
}