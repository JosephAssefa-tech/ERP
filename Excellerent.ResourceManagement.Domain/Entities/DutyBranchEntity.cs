
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class DutyBranchEntity : BaseEntity<DutyBranch>
    {
        [Required]
        public Guid CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        public string Name { get; set; }

        public DutyBranchEntity()
        {

        }


        public DutyBranchEntity(DutyBranch dutyBranch) : base(dutyBranch)
        {
            Guid = dutyBranch.Guid;
            Name = dutyBranch.Name;
            CountryId = dutyBranch.CountryId;
            CreatedDate = dutyBranch.CreatedDate;
            CreatedbyUserGuid = dutyBranch.CreatedbyUserGuid;
            IsActive = dutyBranch.IsActive;
            IsDeleted = dutyBranch.IsDeleted;
        }


        public override DutyBranch MapToModel()
        {
            DutyBranch dutyBranch = new DutyBranch();
            dutyBranch.Guid = Guid;
            dutyBranch.CountryId = CountryId;
            dutyBranch.Name = Name;

            return dutyBranch;
        }

        public override DutyBranch MapToModel(DutyBranch t)
        {
            DutyBranch dutyBranch = t;
            dutyBranch.CountryId = CountryId;
            dutyBranch.Name = Name;

            return dutyBranch;
        }
    }
}
