using System;

namespace Excellerent.ApplicantTracking.Presentation.Models.PostModels
{
    public class ApplicantAreaOfInterestPostModelDto
    {
        public Guid PositionToApplyID { get; set; }

        public Guid ProficiencyLevelID { get; set; }

        public int YearsOfExpierence { get; set; }

        public int MonthOfExpierence { get; set; }
        public Guid ApplicantId { get; set; }
        public string[] SelectedIDArray { get; set; }
        public string[] SelectedIDSecondArray { get; set; }
        public string[] SelectedIDOtherArray { get; set; }
        //public IEnumerable<LUSkillSet> LUSkillSetCollection { get; set; }

    }
}
