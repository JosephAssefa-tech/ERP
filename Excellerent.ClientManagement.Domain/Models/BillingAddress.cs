using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class BillingAddress : BaseAuditModel
    {
        public string Name { get; set; }
        public string Affliation { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public Guid ClientDetailsGuid { get; set; }

    }
}