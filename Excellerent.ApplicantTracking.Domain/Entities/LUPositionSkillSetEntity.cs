
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class LUPositionSkillSetEntity : BaseEntity<LUPositionSkillSet>
    {
        public Guid LUPositionToApplyId { get; set; }
        public Guid LUSkillSetId { get; set; }
        public LUPositionSkillSetEntity()
        {

        }
        public LUPositionSkillSetEntity(LUPositionSkillSet luPositionSkillSet) :base(luPositionSkillSet)
        {
            Guid = luPositionSkillSet.Guid;
            LUPositionToApplyId = luPositionSkillSet.LUPositionToApplyId;
            LUSkillSetId = luPositionSkillSet.LUSkillSetId;
            CreatedDate = luPositionSkillSet.CreatedDate;
        }
        public override LUPositionSkillSet MapToModel()
        {
            LUPositionSkillSet luPositionSkillSet = new LUPositionSkillSet();
            luPositionSkillSet.Guid = Guid.NewGuid();
            luPositionSkillSet.CreatedDate = CreatedDate;
            luPositionSkillSet.LUPositionToApplyId = LUPositionToApplyId;
            luPositionSkillSet.LUSkillSetId = LUSkillSetId;
            luPositionSkillSet.IsActive = IsActive;
            luPositionSkillSet.IsDeleted = IsDeleted;
            return luPositionSkillSet;
        }

        public override LUPositionSkillSet MapToModel(LUPositionSkillSet t)
        {
            LUPositionSkillSet luPositionSkillSet = new LUPositionSkillSet();
            luPositionSkillSet.Guid = Guid;
            luPositionSkillSet.CreatedDate = CreatedDate;
            luPositionSkillSet.LUPositionToApplyId = LUPositionToApplyId;
            luPositionSkillSet.LUSkillSetId = LUSkillSetId;
            luPositionSkillSet.IsActive = IsActive;
            luPositionSkillSet.IsDeleted = IsDeleted;
            return luPositionSkillSet;
        }
    }
}
