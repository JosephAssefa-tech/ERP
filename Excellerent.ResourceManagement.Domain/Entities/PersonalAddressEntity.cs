using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class PersonalAddressEntity : BaseEntity<PersonalAddress>
    {
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string StateRegionProvice { get; set; }
        public string City { get; set; }
        public string SubCityZone { get; set; }
        public string Woreda { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public override PersonalAddress MapToModel()
        {
            PersonalAddress t = new PersonalAddress();

            t.Guid = Guid;
            t.PhoneNumber = PhoneNumber;
            t.Country = Country;
            t.StateRegionProvice = StateRegionProvice;
            t.City = City;
            t.SubCityZone = SubCityZone;
            t.Woreda = Woreda;
            t.HouseNumber = HouseNumber;
            t.PostalCode = PostalCode;
            t.IsActive = IsActive;
            t.IsDeleted = IsDeleted;
            t.CreatedDate = CreatedDate;
            t.CreatedbyUserGuid = CreatedbyUserGuid;

            return t;
        }

        public override PersonalAddress MapToModel(PersonalAddress t)
        {
            t.Guid = Guid;
            t.PhoneNumber = PhoneNumber;
            t.Country = Country;
            t.StateRegionProvice = StateRegionProvice;
            t.City = City;
            t.SubCityZone = SubCityZone;
            t.Woreda = Woreda;
            t.HouseNumber = HouseNumber;
            t.PostalCode = PostalCode;
            t.IsActive = IsActive;
            t.IsDeleted = IsDeleted;
            t.CreatedDate = CreatedDate;
            t.CreatedbyUserGuid = CreatedbyUserGuid;

            return t;
        }
    }
}
