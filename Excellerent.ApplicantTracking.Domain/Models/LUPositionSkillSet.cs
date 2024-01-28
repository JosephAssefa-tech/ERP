
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Models
{
    public class LUPositionSkillSet: BaseAuditModel
    {
        public Guid LUPositionToApplyId { get; set; }
        public LUPositionToApply LUPositionToApply { get; set; }
        public Guid LUSkillSetId { get; set; }
        public LUSkillSet LUSkillSet { get; set; }
        
    }
}
