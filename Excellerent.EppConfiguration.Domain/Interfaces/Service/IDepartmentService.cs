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
    public interface IDepartmentService : ICRUD<DepartmentEntity, Department>
    {
        Task<ResponseDTO> Get(Guid id);
        Task<bool> CheckIfDepartmentExist(string name);
        Task<ResponseDTO> All();
        Task<ResponseDTO> DeleteDepartment(Guid id);
        Task<PredicatedResponseDTO> GetWithPredicate(string searchKey, int? pageIndex, int? pageSize, string? sortBy, SortOrder? sortOrder);
    }
}
