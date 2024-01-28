using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class PersonalAddress : BaseAuditModel
    {
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string StateRegionProvice { get; set; }
        public string City { get; set; }
        public string SubCityZone { get; set; }
        public string Woreda { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
    }
}
