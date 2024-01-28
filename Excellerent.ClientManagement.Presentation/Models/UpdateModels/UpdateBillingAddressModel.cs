using Excellerent.ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Models.UpdateModels
{
   public class UpdateBillingAddressModel
    {
        public Guid Guid { get; set; }
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
        public static BillingAddressEntity MappToEntity(this UpdateBillingAddressModel model)
        {
            BillingAddressEntity entity = new BillingAddressEntity();
            entity.Guid = model.Guid;
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