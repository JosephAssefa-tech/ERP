using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class ApplicantAreaOfInterestEntities : BaseEntity<ApplicantAreaOfInterest>
    {
        public Guid PositionToApplyID { get; set; }

        public LUPositionToApply PositionToApply { get; set; }

        public Guid ProficiencyLevelID { get; set; }

        public LUProficiencyLevel ProficiencyLevel { get; set; }

        public int YearsOfExpierence { get; set; }

        public int MonthOfExpierence { get; set; }
        public string PrimarySkillSetID { get; set; }
        public string SecondarySkillSetID { get; set; }
        public string OtherSkillSet { get; set; }

        public Applicant Applicant { get; set; }

        public Guid ApplicantId { get; set; }
        //[NotMapped]
        //public IEnumerable<LUSkillSet> LUSkillSetCollection { get; set; }

        [NotMapped]
        public string[] SelectedIDArray { get; set; }
         [NotMapped]
        public string[] SelectedIDSecondArray { get; set; }
         [NotMapped]
        public string[] SelectedIDOtherArray { get; set; }
        public ApplicantAreaOfInterestEntities()
        {
        }
        public ApplicantAreaOfInterestEntities(ApplicantAreaOfInterest applicantInterest) : base(applicantInterest)
        {
            Guid = applicantInterest.Guid;
            CreatedDate = applicantInterest.CreatedDate;
            PositionToApplyID = applicantInterest.PositionToApplyID;
            ProficiencyLevelID = applicantInterest.ProficiencyLevelID;
            SelectedIDArray = applicantInterest.PrimarySkillSetID.Split(',').ToArray();
            SelectedIDSecondArray = applicantInterest.SecondarySkillSetID.Split(',').ToArray();
            SelectedIDOtherArray = applicantInterest.OtherSkillSet.Split(',').ToArray();
            YearsOfExpierence = applicantInterest.YearsOfExpierence;
            MonthOfExpierence = applicantInterest.MonthOfExpierence;
            ApplicantId = applicantInterest.ApplicantId;


        }

        public override ApplicantAreaOfInterest MapToModel()
        {
            ApplicantAreaOfInterest applicantInterest = new ApplicantAreaOfInterest();
            applicantInterest.Guid = Guid.NewGuid();
            applicantInterest.CreatedDate = CreatedDate;
            applicantInterest.PositionToApplyID = PositionToApplyID;
            applicantInterest.ProficiencyLevelID = ProficiencyLevelID;
            applicantInterest.PrimarySkillSetID = string.Join(",", SelectedIDArray);
            applicantInterest.SecondarySkillSetID = string.Join(",", SelectedIDSecondArray);
            applicantInterest.OtherSkillSet = string.Join(",", SelectedIDOtherArray);
            applicantInterest.YearsOfExpierence = YearsOfExpierence;
            applicantInterest.MonthOfExpierence = MonthOfExpierence;
            applicantInterest.ApplicantId = ApplicantId;
            applicantInterest.IsActive = IsActive;
            applicantInterest.IsDeleted = IsDeleted;
            return applicantInterest;
        }

        public override ApplicantAreaOfInterest MapToModel(ApplicantAreaOfInterest t)
        {
            ApplicantAreaOfInterest applicantToUpdate = t;
            applicantToUpdate.Guid = Guid;
            applicantToUpdate.CreatedDate=CreatedDate;
            applicantToUpdate.PositionToApplyID = PositionToApplyID;
            applicantToUpdate.ProficiencyLevelID = ProficiencyLevelID;
            applicantToUpdate.PrimarySkillSetID = string.Join(",", SelectedIDArray);
            applicantToUpdate.SecondarySkillSetID = string.Join(",", SelectedIDSecondArray);
            applicantToUpdate.OtherSkillSet= string.Join(",", SelectedIDOtherArray);
            applicantToUpdate.MonthOfExpierence = MonthOfExpierence;
            applicantToUpdate.YearsOfExpierence = YearsOfExpierence;
            applicantToUpdate.ApplicantId = ApplicantId;
            applicantToUpdate.IsActive = IsActive;
            applicantToUpdate.IsDeleted = IsDeleted;
            applicantToUpdate.IsActive = IsActive;
            applicantToUpdate.IsDeleted = IsDeleted;
            return applicantToUpdate;
        }

    }
}

