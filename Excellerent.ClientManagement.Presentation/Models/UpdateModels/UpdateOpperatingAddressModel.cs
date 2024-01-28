using Excellerent.ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Models.UpdateModels
{
    public class UpdateOpperatingAddressModel
    {
        public Guid Guid { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }

    public static class OperatingAddressMapper
    {
        public static OperatingAddressEntity MappToEntity(this UpdateOpperatingAddressModel model)
        {
            OperatingAddressEntity entity = new OperatingAddressEntity();
            entity.Guid = model.Guid;
            entity.Country = model.Country;
            entity.City = model.City;
            entity.State = model.State;
            entity.ZipCode = model.ZipCode;
            entity.Address = model.Address;
            return entity;
        }
    }
}