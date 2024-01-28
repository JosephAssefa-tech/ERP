using Excellerent.SharedModules.Seed;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.ApplicantTracking.Domain.Models
{
    public class Education : BaseAuditModel
    {
       [ForeignKey(nameof(ApplicantId))]
       public virtual Applicant Applicant { get; set; }
       public Guid ApplicantId { get; set; }
       public  Guid EducationProgrammeId { get; set; }
       
       [ForeignKey(nameof(EducationProgrammeId))]
       public LUEducationProgram EducationProgramme { get; set; }
       public string Institution { get; set; }
       [ForeignKey(nameof(FieldOfStudyId))]
       public virtual  LUFieldOfStudy FieldOfStudy { get; set; }
       public Guid? FieldOfStudyId { get; set; }
       public DateTime DateFrom { get; set; }
       public DateTime DateTo { get; set; }
       public bool IsCompleted { get; set; }
       public string OtherFieldOfStudy { get; set; }
       public string Country { get; set; }
       
    }
}
