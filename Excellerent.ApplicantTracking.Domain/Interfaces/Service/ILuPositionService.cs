using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Service
{
    public interface ILuPositionService : ICRUD<LUPositionToApplyEntity, LUPositionToApply>
    {

        Task<Guid> AddPosition(LUPositionToApplyEntity model);

        Task<LUPositionToApplyEntity> GetByPositionName(string PositionName);

        Task<IEnumerable<LUPositionToApplyEntity>> GetAllPositions();
    }
}
