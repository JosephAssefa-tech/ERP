using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class EducationEntity : BaseEntity<Education>
    {
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }
        public EducationProgrammeEntity EducationProgramme { get; set; }
        public Guid EducationProgrammeId { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public LUFieldOfStudyEntity FieldOfStudy { get; set; }
        public Guid? FieldOfStudyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsCompleted { get; set; }
        public string OtherFieldOfStudy { get; set; }
        public EducationEntity()
        {

        }
        public EducationEntity(Education education) :base(education)
        {
            EducationProgramme = education.EducationProgramme == null?null: new EducationProgrammeEntity( education.EducationProgramme);
            Institution = education.Institution;
            Country = education.Country;
            FieldOfStudy = education.FieldOfStudy==null? null: new LUFieldOfStudyEntity(education.FieldOfStudy);
            DateFrom = education.DateFrom;
            DateTo = education.DateTo;
            IsCompleted = education.IsCompleted;
            OtherFieldOfStudy = education.OtherFieldOfStudy;
            Applicant = education.Applicant;
            ApplicantId = education.ApplicantId;
            FieldOfStudyId = education.FieldOfStudyId;
            EducationProgrammeId = education.EducationProgrammeId;
            EducationProgramme = education.EducationProgramme==null? null: new EducationProgrammeEntity(education.EducationProgramme);
            Guid = education.Guid;
        }
        public override Education MapToModel()
        {
            Education education = new Education();
            education.Guid = this.Guid;
            education.EducationProgramme = this.EducationProgramme?.MapToModel();
            education.Institution = this.Institution;
            education.Country = this.Country;
            education.FieldOfStudy = this.FieldOfStudy?.MapToModel();
            education.DateFrom = this.DateFrom;
            education.DateTo = this.DateTo;
            education.IsCompleted = this.IsCompleted;
            education.OtherFieldOfStudy = this.OtherFieldOfStudy;
            education.Applicant = this.Applicant;
            education.ApplicantId = this.ApplicantId;
            education.FieldOfStudy = this.FieldOfStudy?.MapToModel();
            education.FieldOfStudyId = this.FieldOfStudyId;
            education.EducationProgramme = this.EducationProgramme?.MapToModel();
            education.EducationProgrammeId = this.EducationProgrammeId;
            return education;
        }

        public override Education MapToModel(Education education)
        {
            education.Institution = this.Institution;
            education.Country = this.Country;
            education.DateFrom = this.DateFrom;
            education.DateTo = this.DateTo;
            education.IsCompleted = this.IsCompleted;
            education.OtherFieldOfStudy = this.OtherFieldOfStudy;
            education.FieldOfStudyId = this.FieldOfStudyId;
            education.EducationProgrammeId = this.EducationProgrammeId;
            return education;
        }
    }
}
