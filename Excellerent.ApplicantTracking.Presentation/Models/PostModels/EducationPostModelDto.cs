using System;

namespace Excellerent.ApplicantTracking.Presentation.Models.PostModels
{
    public class EducationPostModelDto
    {
        public Guid ApplicantId { get; set; }
        public Guid EducationProgrammeId { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public Guid? FieldOfStudyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsCompleted { get; set; }
        public string OtherFieldOfStudy { get; set; }
        public Guid CreatedbyUserGuid { get; set; }

    }
}
