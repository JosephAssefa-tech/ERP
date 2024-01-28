using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class EmergencyContactsEntity : BaseEntity<EmergencyContactsModel>
    {
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string? phoneNumber2 { get; set; }
        public string? phoneNumber3 { get; set; }
        public string email { get; set; }
        public string? email2 { get; set; }
        public string? email3 { get; set; }
        public string? Country { get; set; }
        public string stateRegionProvice { get; set; }
        public string city { get; set; }
        public string subCityZone { get; set; }
        public string woreda { get; set; }
        public string houseNumber { get; set; }
        public string postalCode { get; set; }

        public EmergencyContactsEntity()
        {

        }
        public EmergencyContactsEntity(EmergencyContactsModel m) : base(m)
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
            //IsActive = m.IsActive;
            //IsDeleted = m.IsDeleted;
            //CreatedDate = m.CreatedDate;
            //CreatedbyUserGuid = m.CreatedbyUserGuid;



        }
        public override EmergencyContactsModel MapToModel()
        {
            EmergencyContactsModel econtact = new EmergencyContactsModel();
            econtact.CreatedDate = CreatedDate;
            econtact.FirstName = FirstName;
            econtact.FatherName = FatherName;
            econtact.GrandFatherName = GrandFatherName;
            econtact.Relationship = Relationship;
            econtact.Guid = Guid;
            econtact.PhoneNumber = PhoneNumber;
            econtact.phoneNumber2 = phoneNumber2;
            econtact.phoneNumber3 = phoneNumber3;
            econtact.email = email;
            econtact.email2 = email2;
            econtact.email3 = email3;
            econtact.Country = Country;
            econtact.stateRegionProvice = stateRegionProvice;
            econtact.city = city;
            econtact.subCityZone = subCityZone;
            econtact.woreda = woreda;
            econtact.houseNumber = houseNumber;
            econtact.postalCode = postalCode;
            //econtact.IsActive = IsActive;
            //econtact.IsDeleted = IsDeleted;
            //econtact.CreatedDate = CreatedDate;
            //econtact.CreatedbyUserGuid = CreatedbyUserGuid;


            return econtact;


        }

        public override EmergencyContactsModel MapToModel(EmergencyContactsModel t)
        {
            EmergencyContactsModel econtact = new EmergencyContactsModel();
            econtact.CreatedDate = t.CreatedDate;
            econtact.FirstName = t.FirstName;
            econtact.FatherName = t.FatherName;
            econtact.GrandFatherName =t. GrandFatherName;
            econtact.Relationship =t. Relationship;
            econtact.Guid =t. Guid;
            econtact.PhoneNumber =t. PhoneNumber;
            econtact.phoneNumber2 =t. phoneNumber2;
            econtact.phoneNumber3 =t. phoneNumber3;
            econtact.email =t. email;
            econtact.email2 =t. email2;
            econtact.email3 =t. email3;
            econtact.Country =t. Country;
            econtact.stateRegionProvice =t. stateRegionProvice;
            econtact.city =t. city;
            econtact.subCityZone =t. subCityZone;
            econtact.woreda =t. woreda;
            econtact.houseNumber =t. houseNumber;
            econtact.postalCode =t. postalCode;
            //econtact.IsActive =t. IsActive;
            //econtact.IsDeleted =t. IsDeleted;
            //econtact.CreatedDate =t. CreatedDate;
            //econtact.CreatedbyUserGuid =t. CreatedbyUserGuid;

            return econtact;
        }
    }
}
