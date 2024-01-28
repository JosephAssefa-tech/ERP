using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Entities
{

    public class LUProficiencyLevelEntity : BaseEntity<LUProficiencyLevel>
    {

        public string Name { get; set; }

        public LUProficiencyLevelEntity()
        {
        }
        public LUProficiencyLevelEntity(LUProficiencyLevel proficencyLevel) : base(proficencyLevel)
        {
            Guid = proficencyLevel.Guid;
            CreatedDate = proficencyLevel.CreatedDate;
            Name = proficencyLevel.Name;
        }

        public override LUProficiencyLevel MapToModel()
        {
            LUProficiencyLevel luProficiencyLevel = new LUProficiencyLevel();
            luProficiencyLevel.Guid = Guid;
            luProficiencyLevel.CreatedDate = CreatedDate;
            luProficiencyLevel.Name = Name;
            luProficiencyLevel.IsActive = IsActive;
            luProficiencyLevel.IsDeleted = IsDeleted;
            return luProficiencyLevel;
        }

        public override LUProficiencyLevel MapToModel(LUProficiencyLevel t)
        {
            LUProficiencyLevel luProficiencyLevelUpdate = t;
            Guid = luProficiencyLevelUpdate.Guid;
            CreatedDate = luProficiencyLevelUpdate.CreatedDate;
            Name = luProficiencyLevelUpdate.Name;
            luProficiencyLevelUpdate.IsActive = IsActive;
            luProficiencyLevelUpdate.IsDeleted = IsDeleted;
            return luProficiencyLevelUpdate;
        }
    }
}
