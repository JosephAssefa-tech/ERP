using Excellerent.ClientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Models.UpdateModels
{
    public class UpdateClientContactModel
    {
        public Guid Guid { get; set; }
        public string ContactPersonName { get; set; }

        public string PhoneNumberPrefix { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public static class ClientContactMapper
    {
        public static ClientContactEntity MappToEntity(this UpdateClientContactModel model)
        {
            ClientContactEntity entity = new ClientContactEntity();
            entity.Guid = model.Guid;
            entity.ContactPersonName = model.ContactPersonName;
            entity.PhoneNumberPrefix = model.PhoneNumberPrefix;
            entity.Email = model.Email;
            entity.PhoneNumber = model.PhoneNumber;

            return entity;
        }
    }
}