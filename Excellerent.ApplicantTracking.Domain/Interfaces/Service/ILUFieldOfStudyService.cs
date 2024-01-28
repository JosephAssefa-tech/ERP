using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{

    public interface ILUFieldOfStudyService : ICRUD<LUFieldOfStudyEntity, LUFieldOfStudy>
    {

        Task<Guid> AddFieldOfStudy(LUFieldOfStudyEntity model);

        Task<LUFieldOfStudyEntity> GetByEducationName(string EducationName);
        Task<LUFieldOfStudyEntity> GetByIdAsync(Guid fieldOfStudyId);
        new Task<IEnumerable<LUFieldOfStudyEntity>> GetAll();

    }
}
