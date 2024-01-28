using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface IEducationProgrammeService : ICRUD<EducationProgrammeEntity, LUEducationProgram>
    {
        Task<LUEducationProgram> GetByIdAsync(Guid educationProgrammId);

        new Task<IEnumerable<EducationProgrammeEntity>> GetAll();
        Task<ResponseDTO> AddAsync(EducationProgrammeEntity entity);

    }
}
