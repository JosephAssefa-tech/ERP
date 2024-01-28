using Excellerent.ResourceManagement.Domain.Models;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class EmergencyAddressEntity : BaseAddressEntity<EmergencyAddress>
    {
        public override EmergencyAddress MapToModel()
        {
            EmergencyAddress t = new EmergencyAddress();

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

        public override EmergencyAddress MapToModel(EmergencyAddress t)
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
