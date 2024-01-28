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
    public class DepartmentService : CRUD<DepartmentEntity, Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleRepository _roleRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IRoleRepository roleRepository) : base(departmentRepository)
        {
            this._departmentRepository = departmentRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ResponseDTO> Get(Guid id)
        {
            Department m = await _departmentRepository.Get(id);
            DepartmentEntity e = new DepartmentEntity(m);
            return new ResponseDTO
            {
                Data = e,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<bool> CheckIfDepartmentExist(string name)
        {
            return await _departmentRepository.CheckIfDepartmentExist(name);
        }

        public async Task<ResponseDTO> All()
        {
            IEnumerable<Department> departments = await _departmentRepository.GetAllAsync();
            return new ResponseDTO
            {
                Data = departments,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<ResponseDTO> DeleteDepartment(Guid id)
        {
            var departmentRoles =await _roleRepository.Count(x => x.DepartmentGuid == id);
            if (departmentRoles == 0) {
                var d= await _departmentRepository.FindOneAsyncForDelete(d => d.Guid == id);
                await _departmentRepository.DeleteAsync(d);
                return (d != null) ? new ResponseDTO(ResponseStatus.Success, "Department Deleted successfully.", d) : new ResponseDTO(ResponseStatus.Error, "Department doesn't exist", null);
            }
            else
            {
                return new ResponseDTO(ResponseStatus.Error, "Cannot delete Department with roles associated to it", null);
            }
        }

        public async Task<PredicatedResponseDTO> GetWithPredicate(string searchKey, int? pageIndex, int? pageSize, string? sortBy, SortOrder? sortOrder)
        {
            int ItemsPerPage = pageSize ?? 10;
            int PageIndex = pageIndex ?? 1;
            var predicate = PredicateBuilder.True<Department>();

            predicate = string.IsNullOrEmpty(searchKey) ? null : predicate.And(p => p.Name.ToLower().Contains(searchKey.ToLower()));

            var departments = (await _departmentRepository.GetWithPredicateAsync(predicate, PageIndex, ItemsPerPage, sortBy, sortOrder))
                    .Select(d => new DepartmentEntity(d)).ToList();

            int TotalRowCount = await _departmentRepository.Count(predicate);
            return new PredicatedResponseDTO
            {
                Data = departments,
                TotalRecord = TotalRowCount,
                PageIndex = PageIndex,
                PageSize = ItemsPerPage,
                TotalPage = TotalRowCount % ItemsPerPage == 0 ? TotalRowCount / ItemsPerPage : TotalRowCount / ItemsPerPage + 1
            };

        }

    }
}
