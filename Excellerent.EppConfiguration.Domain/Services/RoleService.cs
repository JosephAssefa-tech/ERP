using Excellerent.EppConfiguration.Domain.Entities;
using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using Excellerent.EppConfiguration.Domain.Interfaces.Service;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Services
{
    public class RoleService : CRUD<RoleEntity, Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<ResponseDTO> Get(Guid id)
        {
            Role m = await _roleRepository.Get(id);
            RoleEntity e = new RoleEntity(m);
            return new ResponseDTO
            {
                Data = e,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<bool> CheckIfJobTitleExist(string name, Guid departmentGuid)
        {
            return await _roleRepository.CheckIfJobTitleExist(name, departmentGuid);
        }

        public async Task<ResponseDTO> GetRolesByDepartment(Guid departmentId)
        {
            IEnumerable<Role> roles = await _roleRepository.FindAsync(x=>x.DepartmentGuid == departmentId);
            return new ResponseDTO
            {
                Data = roles,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<Role> FindOneAsyncForDelete(Guid id)
        {
            return await _roleRepository.FindOneAsyncForDelete(d => d.Guid == id);
        }

        public async Task<PredicatedResponseDTO> GetWithPredicate(string searchKey, int? pageIndex, int? pageSize, string? sortBy, SortOrder? sortOrder)
        {
            int ItemsPerPage = pageSize ?? 10;
            int PageIndex = pageIndex ?? 1;
            var predicate = PredicateBuilder.True<Role>();

            predicate = string.IsNullOrEmpty(searchKey) ? null : predicate.And(p => p.Name.ToLower().Contains(searchKey.ToLower()));

            var roles = (await _roleRepository.GetWithPredicateAsync(predicate, PageIndex, ItemsPerPage, sortBy, sortOrder))
                    .Select(d => new RoleEntity(d).MapToDto()).ToList();

            int TotalRowCount = await _roleRepository.Count(predicate);
            return new PredicatedResponseDTO
            {
                Data = roles,
                TotalRecord = TotalRowCount,
                PageIndex = PageIndex,
                PageSize = ItemsPerPage,
                TotalPage = TotalRowCount % ItemsPerPage == 0 ? TotalRowCount / ItemsPerPage : TotalRowCount / ItemsPerPage + 1
            };

        }
    }
}
