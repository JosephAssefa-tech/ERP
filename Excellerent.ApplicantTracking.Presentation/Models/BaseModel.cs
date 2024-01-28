using System;

namespace Excellerent.ApplicantTracking.Presentation.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
        }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedbyUserGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastUpdateByUserGuid { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string ProfileImage { get; set; }
        public string ResumeFile { get; set; }

        public abstract T MapToEntity<T>() where T : class;
    }
}
