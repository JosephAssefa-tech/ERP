using System;

namespace Excellerent.ApplicantTracking.Presentation.Models.ViewModels
{
    public class EducationGetModelDto
    {
        public Guid Guid { get; set; }
        public Guid ApplicantId { get; set; }
        public EducationProgrammeGetModelDto EducationProgramme { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public LUFieldOfStudyGetModelDto FieldOfStudy { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsCompleted { get; set; }
        public string OtherFieldOfStudy { get; set; }
    }
}
