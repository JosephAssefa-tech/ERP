using Excellerent.ClientManagement.Domain.Entities;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class OperatingAddressPostModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }

    public static class OperatingAddressMapper
    {
        public static OperatingAddressEntity MappToEntity(this OperatingAddressPostModel model)
        {
            OperatingAddressEntity entity = new OperatingAddressEntity();
            entity.Country = model.Country;
            entity.City = model.City;
            entity.State = model.State;
            entity.ZipCode = model.ZipCode;
            entity.Address = model.Address;
            return entity;
        }
    }
}