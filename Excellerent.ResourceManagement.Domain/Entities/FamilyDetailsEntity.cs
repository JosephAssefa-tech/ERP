using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class FamilyDetailsEntity: BaseEntity<FamilyDetails>
    {
        public Guid RelationshipId { get; set; }
        public Guid EmployeeId { get; set; }
        public Relationship familyMember { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? DoB { get; set; }
        public string Remark { get; set; }

        public FamilyDetailsEntity()
        {
        }

        public FamilyDetailsEntity(FamilyDetails familyDetail) : base(familyDetail)
        {
            this.Guid = familyDetail.Guid;
            this.EmployeeId = familyDetail.EmployeeId;
            this.RelationshipId = familyDetail.RelationshipId;
            this.FullName = familyDetail.FullName;
            this.Gender = familyDetail.Gender;
            this.DoB = familyDetail.DoB;
            this.Remark = familyDetail.Remark;
            CreatedDate = familyDetail.CreatedDate;
            CreatedbyUserGuid = familyDetail.CreatedbyUserGuid;
            IsActive = familyDetail.IsActive;
            IsDeleted = familyDetail.IsDeleted;

        }
        public override FamilyDetails MapToModel()
        {
            FamilyDetails familyDetail = new FamilyDetails();
            familyDetail.Guid = Guid;
            familyDetail.EmployeeId = EmployeeId;
            familyDetail.RelationshipId = RelationshipId;
            familyDetail.FullName = FullName;
            familyDetail.Gender = Gender;
            familyDetail.DoB = DoB;
            familyDetail.Remark = Remark;
            familyDetail.CreatedDate = CreatedDate;
            familyDetail.CreatedbyUserGuid = CreatedbyUserGuid;
            familyDetail.IsActive = IsActive;
            familyDetail.IsDeleted = IsDeleted;

            return familyDetail;

        }

        public override FamilyDetails MapToModel(FamilyDetails t)
        {
            FamilyDetails familyDetail = t;
            familyDetail.Guid = Guid;
            familyDetail.EmployeeId = EmployeeId;
            familyDetail.RelationshipId = RelationshipId;
            familyDetail.FullName = FullName;
            familyDetail.Gender = Gender;
            familyDetail.DoB = DoB;
            familyDetail.Remark = Remark;
            familyDetail.CreatedDate = CreatedDate;
            familyDetail.CreatedbyUserGuid = CreatedbyUserGuid;
            familyDetail.IsActive = IsActive;
            familyDetail.IsDeleted = IsDeleted;

            return familyDetail;
        }
    }
}
