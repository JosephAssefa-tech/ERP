using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class LUSkillSetEntity : BaseEntity<LUSkillSet>
    {
        public string Name { get; set; }
        public LUSkillSetEntity()
        {
        }
        public LUSkillSetEntity(LUSkillSet luSkillSet) : base(luSkillSet)
        {
            Guid = luSkillSet.Guid;
            CreatedDate = luSkillSet.CreatedDate;
            Name = luSkillSet.SkillName;
        }

        public override LUSkillSet MapToModel()
        {
            LUSkillSet luSkillSet = new LUSkillSet();
            luSkillSet.Guid = Guid;
            luSkillSet.CreatedDate = CreatedDate;
            luSkillSet.SkillName = Name;
            luSkillSet.IsActive = IsActive;
            luSkillSet.IsDeleted = IsDeleted;
            return luSkillSet;
        }

        public override LUSkillSet MapToModel(LUSkillSet t)
        {
            LUSkillSet luSkillSetUpdate = t;
            Guid = luSkillSetUpdate.Guid;
            CreatedDate = luSkillSetUpdate.CreatedDate;
            Name = luSkillSetUpdate.SkillName;
            luSkillSetUpdate.IsActive = IsActive;
            luSkillSetUpdate.IsDeleted = IsDeleted;
            return luSkillSetUpdate;
        }
    }
}
