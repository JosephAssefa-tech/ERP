using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface ILUProficiencyLevelService : ICRUD<LUProficiencyLevelEntity, LUProficiencyLevel>
    {
        Task<Guid> AddProficiency(LUProficiencyLevelEntity model);

        Task<LUProficiencyLevelEntity> GetByProficiencyName(string ProficiencyLevelName);

        Task<IEnumerable<LUProficiencyLevelEntity>> GetAllProficiencyLevels();
    }
}

