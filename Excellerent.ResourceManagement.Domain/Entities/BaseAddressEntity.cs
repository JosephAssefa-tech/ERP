using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public abstract class BaseAddressEntity<T> : BaseEntity<T> where T : BaseAddressModel
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
