using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.SharedModules.Seed
{
    public class BaseAuditModel
    {
        int? _requestedHashCode;
        public BaseAuditModel()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        [Key]
        public virtual Guid Guid { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedbyUserGuid { get; set; }
        public bool IsTransient()
        {
            return this.Guid == default(Guid);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is BaseAuditModel other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetRealType() != other.GetRealType())
                return false;
            if (obj == null || !(obj is BaseAuditModel))
                return false;
            //if both objects are not yet persisted to database yet , 
            //we can not say they are the same because they dont have ids yet.
            if (other.IsTransient() || this.IsTransient())
                return false;
            return other.Guid == Guid;
        }
        public static bool operator ==(BaseAuditModel left, BaseAuditModel right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(BaseAuditModel left, BaseAuditModel right)
        {
            return !(left == right);
        }
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Guid.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        private Type GetRealType()
        {
            Type type = GetType();
            if (type.ToString().Contains("Castlet.Proxies."))
                return type.BaseType;
            return type;
        }


    }
}
