using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class OperatingAddressEntity : BaseEntity<OperatingAddress>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }

        public OperatingAddressEntity()
        {
            this.IsActive = true;
        }

        public OperatingAddressEntity(OperatingAddress operatingAddress) : base(operatingAddress)
        {
            Country = operatingAddress.Country;
            Guid = operatingAddress.Guid;
            City = operatingAddress.City;
            State = operatingAddress.State;
            ZipCode = operatingAddress.ZipCode;
            Address = operatingAddress.Address;
            IsActive = operatingAddress.IsActive;
            IsDeleted = operatingAddress.IsDeleted;
            CreatedDate = operatingAddress.CreatedDate;
            CreatedbyUserGuid = operatingAddress.CreatedbyUserGuid;
        }

        public override OperatingAddress MapToModel()
        {
            OperatingAddress operatingAddress = new OperatingAddress();
            operatingAddress.Country = Country;
            operatingAddress.City = City;
            operatingAddress.State = State;
            operatingAddress.ZipCode = ZipCode;
            operatingAddress.Address = Address;
            operatingAddress.IsActive = IsActive;
            operatingAddress.IsDeleted = IsDeleted;
            operatingAddress.CreatedDate = CreatedDate;
            operatingAddress.CreatedbyUserGuid = CreatedbyUserGuid;

            return operatingAddress;
        }

        public override OperatingAddress MapToModel(OperatingAddress t)
        {
            OperatingAddress operatingAddress = t;
            operatingAddress.Country = Country;
            operatingAddress.City = City;
            operatingAddress.State = State;
            operatingAddress.ZipCode = ZipCode;
            operatingAddress.Address = Address;
            operatingAddress.IsActive = IsActive;
            operatingAddress.IsDeleted = IsDeleted;
            operatingAddress.CreatedDate = CreatedDate;
            operatingAddress.CreatedbyUserGuid = CreatedbyUserGuid;

            return operatingAddress;
        }
    }
}