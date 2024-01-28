using Excellerent.EppConfiguration.Domain.Entities;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Interfaces.Service
{
    public interface IRoleService : ICRUD<RoleEntity, Role>
    {
        Task<ResponseDTO> Get(Guid id);
        Task<bool> CheckIfJobTitleExist(string name, Guid departmentGuid);

        Task<ResponseDTO> GetRolesByDepartment(Guid departmentId);

        Task<Role> FindOneAsyncForDelete(Guid id);
        Task<PredicatedResponseDTO> GetWithPredicate(string searchKey, int? pageIndex, int? pageSize, string? sortBy, SortOrder? sortOrder);
    }
}
