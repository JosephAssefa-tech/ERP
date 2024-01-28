using System;

namespace Excellerent.ProjectManagement.Presentation
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
        }

        public virtual Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedbyUserGuid { get; set; }
    }
}