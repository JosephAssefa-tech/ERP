using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class BillingAddressEntity : BaseEntity<BillingAddress>
    {
        public string Name { get; set; }
        public string Affliation { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }

        public BillingAddressEntity()
        {
            this.Guid = Guid.NewGuid();
            this.IsActive = true;
        }

        public BillingAddressEntity(BillingAddress billingAddress) : base(billingAddress)
        {
            Name = billingAddress.Name;
            Guid = billingAddress.Guid;
            Affliation = billingAddress.Affliation;
            Country = billingAddress.Country;
            City = billingAddress.City;
            State = billingAddress.State;
            ZipCode = billingAddress.ZipCode;
            Address = billingAddress.Address;
            IsActive = billingAddress.IsActive;
            IsDeleted = billingAddress.IsDeleted;
            CreatedDate = billingAddress.CreatedDate;
            CreatedbyUserGuid = billingAddress.CreatedbyUserGuid;
        }

        public override BillingAddress MapToModel()
        {
            BillingAddress billingAddress = new BillingAddress();
            billingAddress.Name = Name;
            billingAddress.Affliation = Affliation;
            billingAddress.Country = Country;
            billingAddress.City = City;
            billingAddress.State = State;
            billingAddress.ZipCode = ZipCode;
            billingAddress.Address = Address;
            billingAddress.IsActive = IsActive;
            billingAddress.IsDeleted = IsDeleted;
            billingAddress.CreatedDate = CreatedDate;
            billingAddress.CreatedbyUserGuid = CreatedbyUserGuid;

            return billingAddress;
        }

        public override BillingAddress MapToModel(BillingAddress t)
        {
            BillingAddress billingAddress = t;
            billingAddress.Name = Name;
            billingAddress.Affliation = Affliation;
            billingAddress.Country = Country;
            billingAddress.City = City;
            billingAddress.State = State;
            billingAddress.ZipCode = ZipCode;
            billingAddress.Address = Address;
            billingAddress.IsActive = IsActive;
            billingAddress.IsDeleted = IsDeleted;
            billingAddress.CreatedDate = CreatedDate;
            billingAddress.CreatedbyUserGuid = CreatedbyUserGuid;

            return billingAddress;
        }
    }
}