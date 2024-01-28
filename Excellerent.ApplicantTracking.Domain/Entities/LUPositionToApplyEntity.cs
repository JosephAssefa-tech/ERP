using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class LUPositionToApplyEntity : BaseEntity<LUPositionToApply>
    {
        public string Name { get; set; }

        public LUPositionToApplyEntity()
        {
        }
        public LUPositionToApplyEntity(LUPositionToApply positionToApply) : base(positionToApply)
        {
            Guid = positionToApply.Guid;
            CreatedDate = positionToApply.CreatedDate;
            Name = positionToApply.Name;
        }

        public override LUPositionToApply MapToModel()
        {
            LUPositionToApply lupositionToApply = new LUPositionToApply();
            lupositionToApply.Guid = Guid;
            lupositionToApply.CreatedDate = CreatedDate;
            lupositionToApply.Name = Name;
            lupositionToApply.IsActive = IsActive;
            lupositionToApply.IsDeleted = IsDeleted;
            return lupositionToApply;
        }

        public override LUPositionToApply MapToModel(LUPositionToApply t)
        {
            LUPositionToApply lupositionToApplyUpdate = t;
            lupositionToApplyUpdate.Guid = Guid;
            lupositionToApplyUpdate.CreatedDate = CreatedDate;
            lupositionToApplyUpdate.Name = Name;
            lupositionToApplyUpdate.IsActive = IsActive;
            lupositionToApplyUpdate.IsDeleted = IsDeleted;
            return lupositionToApplyUpdate;
        }
    }
}

