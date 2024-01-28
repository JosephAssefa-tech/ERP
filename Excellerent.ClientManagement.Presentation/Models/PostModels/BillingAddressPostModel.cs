using Excellerent.ClientManagement.Domain.Entities;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class BillingAddressPostModel
    {
        public string Name { get; set; }
        public string Affliation { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }

    public static class BillingAddressMapper
    {
        public static BillingAddressEntity MappToEntity(this BillingAddressPostModel model)
        {
            BillingAddressEntity entity = new BillingAddressEntity();
            entity.Name = model.Name;
            entity.Affliation = model.Affliation;
            entity.Country = model.Country;
            entity.City = model.City;
            entity.State = model.State;
            entity.ZipCode = model.ZipCode;
            entity.Address = model.Address;
            return entity;
        }
    }
}