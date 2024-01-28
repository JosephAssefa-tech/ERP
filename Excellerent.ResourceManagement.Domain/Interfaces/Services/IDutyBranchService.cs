using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IDutyBranchService : ICRUD<DutyBranchEntity, DutyBranch>
    {
        public Task<IEnumerable<DutyBranch>> GetDutyBranchByCountry(Guid countryId);

        public Task<bool> CheckDutyBranchExistance(Guid countryId, string branchName);

        public Task<IEnumerable<DutyBranch>> GetDutyBranchByCountryName(string name);
    }
}
