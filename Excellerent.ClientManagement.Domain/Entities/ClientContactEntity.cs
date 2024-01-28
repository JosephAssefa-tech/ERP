using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ClientManagement.Domain.Entities
{
    public class ClientContactEntity : BaseEntity<ClientContact>
    {
        public string ContactPersonName { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ClientDetailsGuid { get; set; }


        public ClientContactEntity()
        {
        }

        public ClientContactEntity(ClientContact clientContact)
        {
            ContactPersonName = clientContact.ContactPersonName;
            Guid = clientContact.Guid;
            PhoneNumberPrefix = clientContact.PhoneNumberPrefix;
            Email = clientContact.Email;
            PhoneNumber = clientContact.PhoneNumber;
        }

        public override ClientContact MapToModel()
        {
            ClientContact clientContact = new ClientContact();
           
            clientContact.ContactPersonName = ContactPersonName;
            clientContact.PhoneNumberPrefix = PhoneNumberPrefix;
            clientContact.Email = Email;
            clientContact.PhoneNumber = PhoneNumber;

            return clientContact;
        }

        public override ClientContact MapToModel(ClientContact t)
        {
            ClientContact clientContact = t;
            clientContact.ContactPersonName = ContactPersonName;
            clientContact.PhoneNumberPrefix = PhoneNumberPrefix;
            clientContact.Email = Email;
            clientContact.PhoneNumber = PhoneNumber;

            return clientContact;
        }
    }
}