using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class ApplicantEntity : BaseEntity<Applicant>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string ResumeFile { get; set; }
        public string Status { get; set; }

        public ApplicantEntity() { }

        public ApplicantEntity(Applicant applicant) : base(applicant)
        {
            Guid = applicant.Guid;
            CreatedDate = applicant.CreatedDate;
            FirstName = applicant.FirstName;
            LastName = applicant.LastName;
            Email = applicant.Email;
            Password = applicant.Password;
            ProfileImage = applicant.ProfilePictureUpload;
            ResumeFile = applicant.ResumeUpload;
            Country = applicant.Country;
            ContactNumber = applicant.ContactNumber;
        }

        public override Applicant MapToModel()
        {
            Applicant applicant = new Applicant();
            applicant.FirstName = FirstName;
            applicant.LastName = LastName;
            applicant.CreatedDate = CreatedDate;
            applicant.Guid = Guid.NewGuid();
            applicant.Country = Country;
            applicant.ContactNumber = ContactNumber;
            applicant.ResumeUpload = ResumeFile;
            applicant.ProfilePictureUpload = ProfileImage;
            applicant.Status = Status;
            applicant.Email = Email;
            applicant.Password = Password;
            applicant.IsActive = IsActive;
            applicant.IsDeleted = IsDeleted;
            return applicant;
        }

        public override Applicant MapToModel(Applicant t)
        {
            Applicant applicantToUpdate = t;
            applicantToUpdate.FirstName = FirstName;
            applicantToUpdate.LastName = LastName;
            applicantToUpdate.CreatedDate = CreatedDate;
            applicantToUpdate.Country = Country;
            applicantToUpdate.ContactNumber = ContactNumber;
            applicantToUpdate.ResumeUpload = ResumeFile;
            applicantToUpdate.ProfilePictureUpload = ProfileImage;
            applicantToUpdate.IsActive = IsActive;
            applicantToUpdate.IsDeleted = IsDeleted;
            return applicantToUpdate;
        }


    }
}
