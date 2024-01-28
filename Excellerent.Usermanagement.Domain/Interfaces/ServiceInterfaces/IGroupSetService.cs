
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Excellerent.SharedModules.Seed;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IGroupSetService : ICRUD<GroupSetEntity, GroupSet>
    {
        Task<PredicatedResponseDTO> GetAllUserGroupsDashboardAsync(string searchKey, int? pageindex, int? pageSize, string? sortBY, SortOrder? sortOrder);

        Task<GroupSetEntity> GetGroupByName(string name);

        new Task<ResponseDTO> Update(GroupSetEntity entity);

        Task<ResponseDTO> Delete(Guid id);

        Task<GroupSetDetail> GetGroupSetById(Guid groupId);

        new Task<IEnumerable<GroupSetEntity>> GetAll();

        Task UpdateGroupDescription(GroupSetPatchModel newGroupDescription);

        Task<bool> IsSuperAdmin(string id);


    }
}
