using Excellerent.SharedModules.Seed;

namespace Excellerent.ApplicantTracking.Domain.Models
{
    public class LUSkillPositionAssociation : BaseAuditModel
    {
        public int PositionToApplyID { get; set; }

        public LUPositionToApply luPositionToApply { get; set; }

        public int PrimarySkillSetID { get; set; }

        public LUSkillSet skillSet { get; set; }
        public int SecondarySkillSetID { get; set; }
        public int OtherSkillSet { get; set; }
    }
}
