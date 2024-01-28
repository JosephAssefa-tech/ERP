using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Presentation.Models.PutModels
{
   public class EducationPutModel
    {
        public virtual Guid Guid { get; set; }
        public Guid EducationProgrammeId { get; set; }
        public string Institution { get; set; }
        public Guid? FieldOfStudyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsCompleted { get; set; }
        public string OtherFieldOfStudy { get; set; }
        public string Country { get; set; }

    }
}
