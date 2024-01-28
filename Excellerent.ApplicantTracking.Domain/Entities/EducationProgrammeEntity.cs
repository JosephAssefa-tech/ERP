using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ApplicantTracking.Domain.Entities
{
    public class EducationProgrammeEntity : BaseEntity<LUEducationProgram>
    {
        public string Name { get; set; }

        public EducationProgrammeEntity(LUEducationProgram programme): base(programme)
        {
            this.Name = programme.Name;
        }
        public EducationProgrammeEntity()
        {
        }
        public override LUEducationProgram MapToModel()
        {
            LUEducationProgram programme = new LUEducationProgram();
            programme.Guid = Guid.NewGuid();
            programme.Name = this.Name;
            return programme;
        }

        public override LUEducationProgram MapToModel(LUEducationProgram programme)
        {
            programme.Guid = this.Guid;
            programme.Name = this.Name;
            return programme;
        }
    }
}
