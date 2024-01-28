using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using LinqKit;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.Usermanagement.Domain.Enums;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class UserService : CRUD<UserEntity, User>, IUserService
    {
        public IUserRepository _repository { get; }
        public IEmployeeRepository _emprepository { get; }
        public UserService(IUserRepository repository, IEmployeeRepository emprepository) : base(repository)
        {
            _repository = repository;
            _emprepository = emprepository;
        }

        public async Task<PredicatedResponseDTO> GetUserDashBoardList(string userName, int? pageIndex, int? pageSize, 
            string sortBy, SortOrder? sortOrder, List<string>? departmentFilter, List<string>? jobTitleFilter, List<string>? statusFilter)
        {
            int ItemsPerPage = pageSize ?? 8;
            int PageIndex = pageIndex ?? 1;
            var predicate = PredicateBuilder.True<User>();

            if (String.IsNullOrEmpty(userName) && departmentFilter == null && jobTitleFilter == null && statusFilter == null)
                predicate = null;
            if (!String.IsNullOrEmpty(userName))
            {
                if (userName.Contains(' '))
                {
                    string tmp = userName.Substring(0, userName.IndexOf(' '));
                    string tmp2 = userName.Substring(userName.IndexOf(' '));

                    predicate = predicate.And(p => p.FullName.ToLower().Contains(tmp.ToLower()));
                                     // .And(p => p.FirstName.ToLower().Contains(tmp.ToLower())).Or(p => p.MiddleName.ToLower().Contains(tmp2.ToLower())).Or(p => p.LastName.ToLower().Contains(userName.ToLower()));
                }
                else
                {
                    predicate = predicate.And(p => p.FullName.ToLower().Contains(userName.ToLower()));
                                  // .And(p => p.FirstName.ToLower().Contains(userName.ToLower())).Or(p => p.MiddleName.ToLower().Contains(userName.ToLower())).Or(p => p.LastName.ToLower().Contains(userName.ToLower()));
                }
            }
            if(departmentFilter.Count() > 0)
            {
                var predicateDepartment = PredicateBuilder.New<User>();
                foreach (var a in departmentFilter)
                {
                    predicateDepartment = predicateDepartment.Or(x => x.Employee.EmployeeOrganization.Department.Name.Equals(a));
                }
                predicate = predicate.And(predicateDepartment);
            }

            if (jobTitleFilter.Count() > 0)
            {
                var predicateJobTitle= PredicateBuilder.New<User>();
                foreach (var a in jobTitleFilter)
                {
                    predicateJobTitle = predicateJobTitle.Or(x => x.Employee.EmployeeOrganization.Role.Name.Equals(a));
                }
                predicate = predicate.And(predicateJobTitle);
            }

            if (statusFilter.Count() > 0)
            {
                var predicateStatus= PredicateBuilder.New<User>();
                foreach (var a in statusFilter)
                {
                    var aStatus = a.Equals("Active") ? UserStatus.Active : UserStatus.NotActive;
                    predicateStatus = predicateStatus.Or(x => x.Status.Equals(aStatus));
                }
                predicate = predicate.And(predicateStatus);
            }

            var result = await _repository.GetUserDashBoardList(predicate, PageIndex, ItemsPerPage, sortBy, sortOrder);
            int totalRowCount = await _repository.GetUserDashBoardListCount(predicate);
            return new PredicatedResponseDTO
            {
                Data = result,
                TotalRecord = totalRowCount,//total row count
                PageIndex = PageIndex,
                PageSize = ItemsPerPage,  // itemPerPage,
                TotalPage = totalRowCount % ItemsPerPage == 0 ? totalRowCount / ItemsPerPage : totalRowCount / ItemsPerPage + 1
            };
        }

        public async Task<ResponseDTO> GetDistinctDepartments()
        {
            var result = await _repository.GetDistinctDepartments();
            return new ResponseDTO
            {
                Data = result,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<ResponseDTO> GetDistinctJobTitles()
        {
            var result = await _repository.GetDistinctJobTitles();
            return new ResponseDTO
            {
                Data = result,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            try
            {
                IEnumerable<User> users = await _repository.GetAllAsync();
                IEnumerable<UserEntity> mappedUsers = new List<UserEntity>() ;
                if(users.Any())
                mappedUsers = users.Select(u => new UserEntity(u));
                return mappedUsers;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            try
            {
                User user = await _repository.FindOneAsync(u => u.Guid == id);

                return user ==null ? null:  new UserEntity(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<IEnumerable<User>> GetUserByEmployeeId(Guid empId)
        {
            try
            {
                return await _repository.FindAsync(u => u.EmployeeId == empId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<User> GetUserByEmployeeIdOrEmail(Guid empId, string Email)
        {
            try
            {
                var model = await _repository.FindOneAsync(u => (u.EmployeeId == empId || u.Email == Email));
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<bool> UseExistByEmployeeIdOrEmail(Guid empId, string Email)
        {
            try
            {
                var model = await _repository.FindOneAsync(u => (u.EmployeeId == empId || u.Email == Email) && u.IsDeleted == false);
                return model != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public new async Task<ResponseDTO> Update(UserEntity entity)
        {
            var data = await _repository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            await _repository.UpdateAsync(model);

            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }
        public  async Task<ResponseDTO> CreateUser(UserEntity entity, Guid [] GroupIds)
        {
            var model = entity.MapToModel();
            model.IsPasswordGenerated = true;
            model.LastActivityDate = DateTime.Now;
            bool success =   await _repository.CreatUser(model, GroupIds);
            if(success)
            return new ResponseDTO(ResponseStatus.Success, "User is created successfully", null);
            
            return new ResponseDTO(ResponseStatus.Error, "", null);
        }

        public async Task<ResponseDTO> CreateDeletedUser(UserEntity entity, Guid[] GroupIds)
        {
            var data = await _repository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            model.Password = entity.Password;
            model.IsDeleted = false;
            model.IsActive = true;
            model.Status = Enums.UserStatus.Active;

            model.IsPasswordGenerated = true;
            model.LastActivityDate = DateTime.Now;

            await _repository.UpdateAsync(model);

            //var model = entity.MapToModel();
            
            bool success = await _repository.CreateDeletedUser(model, GroupIds);
            if (success)
                return new ResponseDTO(ResponseStatus.Success, "User is created successfully", null);

            return new ResponseDTO(ResponseStatus.Error, "", null);
        }

        public async Task<ResponseDTO> GetEmployeesNotInUsers()
        {
            try
            {
                var employees = await _repository.GetEmployeesNotInAsUser();
                IEnumerable<EmployeeEntity> employeeEntities = new List<EmployeeEntity>();
                if(employees != null && employees.Any())
                {
                    employeeEntities = employees.Select(e => new EmployeeEntity(e));
                }
                return new ResponseDTO(ResponseStatus.Success, "", employeeEntities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
       
        

        public async Task<Guid> GetUserGuidByEmail(string email)
        {
            var data = await _repository.FindOneAsync(x => x.Email.ToLower() == email.ToLower() && x.IsDeleted == false);
            return data == null ? Guid.Empty : data.Guid;
        }
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            var model = await _repository.FindOneAsync(x => x.Email.ToLower() == email.ToLower());
            if (model == null) return null;
            return new UserEntity(model);

        }

        public async Task<ResponseDTO> LoadUsersNotGroupedInGroup(Guid groupSetId)
        {
            var result = await _repository.LoadUsersNotGroupedInGroup(groupSetId);
            if(result == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "There are no user not assigned to this specific group", null);
            }
            return new ResponseDTO(ResponseStatus.Success, "", result);
        }

        public async Task<UserEntity> ValidateUser(string email)
        {
            try
            {
                User user = await _repository.FindOneAsync(u => u.Email.ToLower() == email.ToLower() && u.IsDeleted == false);

                return ((user == null) || (!user.IsActive)) ? null : new UserEntity(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        public async Task<ResponseDTO> RemoveUser(Guid userId)
        {
            User result = await _repository.GetByGuidAsync(userId);
            if(result == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "Can not get user", null);
            }
            result.Status = Enums.UserStatus.NotActive;
            result.IsActive = false;
            result.IsDeleted = true;
            await _repository.UpdateAsync(result);
           // await _repository.DeleteAsync(result);
            return new ResponseDTO(ResponseStatus.Success, "User deleted successfully", null);
        }

        public async Task<UserEntity> Authenticate(string Email, string Password)
        {
            var data = await _repository.FindOneAsync(x => x.Email.ToLower() == Email.ToLower() && x.IsDeleted == false);
            return data != null ? new UserEntity(data) : null;
        }


        public  async Task<ResponseDTO> ChangePassword(UserEntity entity, string newPassword, bool isPasswordGenerated)
        {
            var data = await _repository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            model.Password = newPassword;
            model.IsPasswordGenerated = isPasswordGenerated;
            await _repository.UpdateAsync(model);

            return new ResponseDTO(ResponseStatus.Success, "Password updated successfully", null);
        }
        public  async Task<ResponseDTO> ResetPassword(string email, string newPassword, bool isPasswordGenerated)
        {
            try
            {
                var model = await _repository.FindOneAsync(x => x.Email.ToLower() == email.ToLower() && x.IsDeleted == false);
                if (model == null)
                {
                    return new ResponseDTO(ResponseStatus.Error, "User not found", null);
                }
                model.Password = newPassword;
                model.IsPasswordGenerated = isPasswordGenerated;
                await _repository.UpdateAsync(model);

                return new ResponseDTO(ResponseStatus.Success, "Password reseted successfully", null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> IsSuperAdmin(string id)
        {

            var model = await _repository.FindOneAsync(x=>x.Guid == Guid.Parse(id));
            if (model == null) return false; 
            var emp =  _emprepository.GetEmployeesById(model.EmployeeId);
            if (emp.EmployeeNumber == "EDC-000") 
            {
                return true;
            }
            return false;
        }
    }
}
