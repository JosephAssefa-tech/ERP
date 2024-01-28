using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ResourceManagement.Domain.Models
{
    //Model
    public  class EmergencyContactsModel :BaseAuditModel
    {
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string phoneNumber2 { get; set; }
        public string phoneNumber3 { get; set; }
        public string email { get; set; }
        public string email2 { get; set; }
        public string email3 { get; set; }
        public string Country { get; set; }
        public string stateRegionProvice { get; set; }
        public string city { get; set; }
        public string subCityZone { get; set; }
        public string woreda { get; set; }
        public string houseNumber { get; set; }
        public string postalCode { get; set; }

        public EmergencyContactsModel(EmergencyContactsEntity m)
        {
            CreatedDate = m.CreatedDate;
            FirstName = m.FirstName;
            FatherName = m.FatherName;
            GrandFatherName = m.GrandFatherName;
            Relationship = m.Relationship;
            Guid = m.Guid;
            PhoneNumber = PhoneNumber;
            phoneNumber2 = phoneNumber2;
            phoneNumber3 = phoneNumber3;
            email = email;
            email2 = email2;
            email3 = email3;
            Country = m.Country;
            stateRegionProvice = m.stateRegionProvice;
            city = m.city;
            subCityZone = m.subCityZone;
            woreda = m.woreda;
            houseNumber = m.houseNumber;
            postalCode = m.postalCode;
            IsActive = m.IsActive;
            IsDeleted = m.IsDeleted;
            CreatedDate = m.CreatedDate;
            CreatedbyUserGuid = m.CreatedbyUserGuid;

        }

        public EmergencyContactsModel()
        {
        }
    }
}
