
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class GroupSetService : CRUD<GroupSetEntity, GroupSet>, IGroupSetService
    {
        private readonly IGroupSetRepository _groupsetRepository;
        private readonly IUserRepository _userRepository;
        public GroupSetService(IGroupSetRepository groupsetRepository) : base(groupsetRepository)
        {
            _groupsetRepository = groupsetRepository;
           
        }

        public async Task<PredicatedResponseDTO> GetAllUserGroupsDashboardAsync(string searchKey, int? pageIndex, int? pageSize, string sortBy, SortOrder? sortOrder)
        {
            int ItemsPerPage = pageSize ?? 8;
            int PageIndex = pageIndex ?? 1;
            var predicate = PredicateBuilder.True<GroupSet>();

            predicate = String.IsNullOrEmpty(searchKey) ? null : predicate.And(p => p.Name.ToLower().Contains(searchKey.ToLower()));

            var groupList = await _groupsetRepository.GetAllUserGroupsDashboardAsync(predicate, PageIndex, ItemsPerPage, sortBy, sortOrder);
            
            int totalRowCount = await _groupsetRepository.AllUserGroupsDashboardCountAsync(predicate);
            return new PredicatedResponseDTO
            {
                Data = groupList,
                TotalRecord = totalRowCount,//total row count
                PageIndex = PageIndex,
                PageSize = ItemsPerPage,  // itemPerPage,
                TotalPage = totalRowCount % ItemsPerPage == 0 ? totalRowCount / ItemsPerPage : totalRowCount / ItemsPerPage + 1
            };
        }

        public new async Task<ResponseDTO> Add(GroupSetEntity entity)
        {
            try
            {
                var model = await _groupsetRepository.AddAsync(entity.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }

        }

        public async Task<GroupSetDetail> GetGroupSetById(Guid groupId)
        {
            return await _groupsetRepository.GetGroupSetById(groupId);
        }

        public new async Task<ResponseDTO> Update(GroupSetEntity entity)
        {
            var data = await _groupsetRepository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "Group not found", null);
            }
            var model = entity.MapToModel(data);
            await _groupsetRepository.UpdateAsync(model);
            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }

        public async Task<ResponseDTO> Delete(Guid id)
        {
            var data = await _groupsetRepository.FindOneAsync(x => x.Guid == id);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "Group not found", null);
            }
            await _groupsetRepository.DeleteAsync(data);
            return new ResponseDTO(ResponseStatus.Success, "Group deleted successfully", null);
        }

        
        public new async Task<IEnumerable<GroupSetEntity>> GetAll()
        {
            var groups = await _groupsetRepository.GetAllAsync();
            return groups.Select(g => new GroupSetEntity(g));
        }

        public async Task UpdateGroupDescription(GroupSetPatchModel newGroupDescription)
        {
            await _groupsetRepository.UpdateGroupDescription(newGroupDescription);
        }

        public async Task<GroupSetEntity> GetGroupByName(string name)
        {
            var groups = await _groupsetRepository.FindOneAsync(x => x.Name.ToLower() == name.ToLower());
            return groups == null ? null : new GroupSetEntity(groups);
        }

        public async Task<bool> IsSuperAdmin(string id)
        {
            var model = await _groupsetRepository.FindOneAsync(x => x.Guid == Guid.Parse(id));
            if (model == null) return false;
            if (model.Name.ToLower() == "admin")
            {
                return true;
            }
            return false;

        }
    }
}
