using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface ILUSkillSetService : ICRUD<LUSkillSetEntity, LUSkillSet>
    {

        Task<Guid> AddSkill(LUSkillSetEntity model);

        Task<LUSkillSetEntity> GetBySkillName(string SkillName);

        Task<IEnumerable<LUSkillSetEntity>> GetAllSkillSet();
    }
}

