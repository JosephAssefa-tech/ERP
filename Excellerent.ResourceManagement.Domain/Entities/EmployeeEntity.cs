using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class EmployeeEntity : BaseEntity<Employee>
    {
        public EmployeeEntity()
        {

        }
        public EmployeeEntity(Employee employee): base(employee)
        {
            Organization = employee.Organization;
            EmployeeNumber = employee.EmployeeNumber;
            // FirstName = employee.FirstName;
            // FatherName = employee.FatherName;
            // GrandFatherName = employee.GrandFatherName;
            FullName = employee.FullName;
            MobilePhone = employee.MobilePhone;
            Phone1 = employee.Phone1;
            Phone2 = employee.Phone2;
            PersonalEmail = employee.PersonalEmail;
            PersonalEmail2 = employee.PersonalEmail2;
            PersonalEmail3 = employee.PersonalEmail3;
            DateofBirth = employee.DateofBirth;
            Gender = employee.Gender;
            Nationality =employee.Nationality;
            Photo = employee.Photo;
            EmergencyContact = employee.EmergencyContact;
            FamilyDetails = employee.FamilyDetails;
            EmployeeOrganization = employee.EmployeeOrganization;
            EmployeeAddress = employee.EmployeeAddress;
        }
        public string Organization { get; set; }

        public string EmployeeNumber { get; set; }

        public string FirstName
        {
            get; set;
        }
        public string FatherName
        {
            get; set;
        }
        public string GrandFatherName
        {
            get; set;
        }
        public string FullName
        {
            get; set;
        }
        public string MobilePhone
        {
            get; set;
        }
        public string Phone1
        {
            get; set;
        }
        public string Phone2
        {
            get; set;
        }
       

        public string PersonalEmail
        {
            get; set;
        }

        public string PersonalEmail2
        {
            get; set;
        }

        public string PersonalEmail3
        {
            get; set;
        }
        public DateTime DateofBirth
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
        public List<Nationality> Nationality { get; set; }
        public string Photo { get; set; }

        public List<EmergencyContactsModel> EmergencyContact { get; set; }
        public List<FamilyDetails> FamilyDetails { get; set; }
        public EmployeeOrganization EmployeeOrganization { get; set; }

        public List<PersonalAddress> EmployeeAddress { get; set; }

        public override Employee MapToModel()
        {
            Employee employee = new Employee();
            return MapToModel(employee);
        }


        public override Employee MapToModel(Employee t)
        {
            t.Organization = Organization;
            t.EmployeeNumber = EmployeeNumber;
            // t.FirstName = FirstName;
            // t.FatherName = FatherName;
            // t.GrandFatherName = GrandFatherName;
            t.FullName = FullName;
            t.MobilePhone = MobilePhone;
            t.Phone1 = Phone1;
            t.Phone2 = Phone2;
            t.PersonalEmail = PersonalEmail;
            t.PersonalEmail2 = PersonalEmail2;
            t.PersonalEmail3 = PersonalEmail3;
            t.DateofBirth = DateofBirth;
            t.Gender = Gender;
            t.Nationality = Nationality;
            t.Photo = Photo;
            t.EmergencyContact = EmergencyContact;
            t.FamilyDetails = FamilyDetails;
            t.EmployeeOrganization = EmployeeOrganization;
            t.EmployeeAddress = EmployeeAddress;

            return t;
        }

    }
}
