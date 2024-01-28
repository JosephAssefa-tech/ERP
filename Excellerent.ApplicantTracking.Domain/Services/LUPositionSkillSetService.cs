using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class LUPositionSkillSetService : CRUD<LUPositionSkillSetEntity, LUPositionSkillSet>, ILUPositionSkillSetService
    {
        private readonly ILUPositionSkillSetRepository _positionSkillRepository;
        public LUPositionSkillSetService(ILUPositionSkillSetRepository positionSkillRepo) : base(positionSkillRepo)
        {
            _positionSkillRepository = positionSkillRepo;
        }
        async public Task<IEnumerable<LUSkillSetEntity>> GetSkillByPosition(Guid guid)
        {
            var data = await _positionSkillRepository.GetSkillsByPosition(guid);
            return data?.Select(x => new LUSkillSetEntity(x));
        }
    }
}
