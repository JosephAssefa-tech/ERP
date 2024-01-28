using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Entities
{

    public class LUFieldOfStudyEntity : BaseEntity<LUFieldOfStudy>
    {
        public string Name { get; set; }

        public LUFieldOfStudyEntity()
        {
        }
        public LUFieldOfStudyEntity(LUFieldOfStudy fieldOfStudy) : base(fieldOfStudy)
        {
            Guid = fieldOfStudy.Guid;
            CreatedDate = fieldOfStudy.CreatedDate;
            Name = fieldOfStudy.EducationName;
        }

        public override LUFieldOfStudy MapToModel()
        {
            LUFieldOfStudy lufieldofstudy = new LUFieldOfStudy();
            lufieldofstudy.Guid = Guid;
            lufieldofstudy.CreatedDate = CreatedDate;
            lufieldofstudy.EducationName = Name;
            lufieldofstudy.IsActive = IsActive;
            lufieldofstudy.IsDeleted = IsDeleted;
            return lufieldofstudy;
        }

        public override LUFieldOfStudy MapToModel(LUFieldOfStudy t)
        {
            LUFieldOfStudy luFieldOfStudyUpdate = t;
            Guid = luFieldOfStudyUpdate.Guid;
            CreatedDate = luFieldOfStudyUpdate.CreatedDate;
            Name = luFieldOfStudyUpdate.EducationName;
            luFieldOfStudyUpdate.IsActive = IsActive;
            luFieldOfStudyUpdate.IsDeleted = IsDeleted;
            return luFieldOfStudyUpdate;
        }
    }
}