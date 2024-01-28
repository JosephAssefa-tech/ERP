using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class ClientContact : BaseAuditModel
    {
        public string ContactPersonName { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ClientDetailsGuid { get; set; }

    }
}