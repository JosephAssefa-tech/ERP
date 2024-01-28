using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class Employee : BaseAuditModel
    {

        
        public Employee()
        {

        }

        [Key]
        public override Guid Guid { get;  set; }

        public string Organization { get; set; }

        public string EmployeeNumber { get; set; }

        // [Required]
        // [MaxLength(25), MinLength(1)]
        // [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string FirstName 
        {
            get; set;
        }
        //[Required]
        //[MaxLength(25), MinLength(1)]
       // [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string FatherName 
        {
            get; set;
        }
        // [Required]
        // [MaxLength(25), MinLength(1)]
        // [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string GrandFatherName 
        {
            get; set;
        }

        [Required]
        [MaxLength(80), MinLength(1)]
        [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string FullName
        {
            get; set;
        }
        [Required]
        [Phone]
        [MaxLength(15), MinLength(9)]
        public string MobilePhone
        {
            get; set;
        }
        [Phone]
        [MaxLength(15), MinLength(9)]
        public string Phone1
        {
            get; set;
        }
        [Phone]
        [MaxLength(15), MinLength(9)]
        public string Phone2
        {
            get; set;
        }
        [Required]
        [EmailAddress]
        public string PersonalEmail
        {
            get; set;
        }
        
        [EmailAddress]
        public string PersonalEmail2
        {
            get; set;
        }
       
        [EmailAddress]
        public string PersonalEmail3
        {
            get; set;
        }
        [Required]
        public DateTime DateofBirth
        {
            get; set;
        }
        [Required]
        public string Gender
        {
            get; set;
        }
        [Required]
        public List<Nationality> Nationality { get; set; }
        public string Photo { get; set; }

        public List<EmergencyContactsModel> EmergencyContact { get; set; }
        public List<FamilyDetails> FamilyDetails { get; set; }
        public EmployeeOrganization EmployeeOrganization { get; set; }

        public List<PersonalAddress> EmployeeAddress { get; set; }


    }
}
