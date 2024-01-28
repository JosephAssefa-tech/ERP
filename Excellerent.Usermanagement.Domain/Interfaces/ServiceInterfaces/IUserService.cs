using Excellerent.SharedModules.Interface.Service;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Excellerent.SharedModules.Seed;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IUserService: ICRUD<UserEntity, User>
    {
        public Task<PredicatedResponseDTO> GetUserDashBoardList(string userName, int? pageindex, int? pageSize, 
            string sortBy, SortOrder? sortOrder, List<string>? departmentFilter, List<string>? jobTitleFilter, List<string>? statusFilter);
        Task<ResponseDTO> GetDistinctDepartments();
        Task<ResponseDTO> GetDistinctJobTitles();
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity> GetUser(Guid id);
        new Task<ResponseDTO> Update(UserEntity entity);
        Task<IEnumerable<User>> GetUserByEmployeeId(Guid empId);
        Task<User> GetUserByEmployeeIdOrEmail(Guid empId, string Email);
        Task<bool> UseExistByEmployeeIdOrEmail(Guid empId, string Email);
        Task<ResponseDTO> GetEmployeesNotInUsers();
        Task<Guid> GetUserGuidByEmail(String email);
        Task<UserEntity> GetUserByEmail(String email);
        Task<ResponseDTO> CreateUser(UserEntity userEntity, Guid[] groupIds);
        Task<ResponseDTO> CreateDeletedUser(UserEntity userEntity, Guid[] groupIds);

        Task<ResponseDTO> LoadUsersNotGroupedInGroup(Guid groupSetId);
        Task<UserEntity> ValidateUser(string email);

        Task<ResponseDTO> RemoveUser(Guid userId);

        Task<UserEntity> Authenticate(string Email, string Password);


        Task<ResponseDTO> ChangePassword(UserEntity entity, string newPassword, bool isPasswordGenerated);
        public Task<ResponseDTO> ResetPassword(string email, string newPassword, bool isPasswordGenerated);

        Task<bool> IsSuperAdmin(string id);


    }
}
