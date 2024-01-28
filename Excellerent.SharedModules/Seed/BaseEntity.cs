using System;

namespace Excellerent.SharedModules.Seed
{
    public abstract class BaseEntity<T> where T : BaseAuditModel
    {
        public virtual Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedbyUserGuid { get; set; }
        public abstract T MapToModel();
        public abstract T MapToModel(T t);
        public BaseEntity()
        {

        }
        public BaseEntity(T auditModel)
        {
            Guid = auditModel.Guid;
            IsActive = auditModel.IsActive;
            CreatedbyUserGuid = auditModel.CreatedbyUserGuid;
            CreatedDate = auditModel.CreatedDate;
        }
    }
}
