using Excellerent.ClientManagement.Domain.Entities;

namespace Excellerent.ClientManagement.Presentation.Models.PostModels
{
    public class ClientContactPostModel
    {
        public string ContactPersonName { get; set; }

        public string PhoneNumberPrefix { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public static class ClientContactMapper
    {
        public static ClientContactEntity MappToEntity(this ClientContactPostModel model)
        {
            ClientContactEntity entity = new ClientContactEntity();
            entity.ContactPersonName = model.ContactPersonName;
            entity.PhoneNumberPrefix = model.PhoneNumberPrefix;
            entity.Email = model.Email;
            entity.PhoneNumber = model.PhoneNumber;

            return entity;
        }
    }
}