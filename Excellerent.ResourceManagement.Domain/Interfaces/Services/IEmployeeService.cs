using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.SharedModules.DTO;
using System.Collections.Generic;

using System.Threading.Tasks;
using System;
using Excellerent.ResourceManagement.Domain.DTOs;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IEmployeeService : ICRUD<EmployeeEntity, Employee>
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Employee GetEmployeesById(Guid empId);
        Employee GetEmployeesByEmpNumber(string empId);
        Task<Employee> GetEmployeesByEmailAsync(string email);
        Task<Employee> AddNewEmployeeEntry(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);

        Task<bool> CheckIfEmailExists(string email);
        Task UpdateEmployee(EmployeeEntity employeeEntity);
        Task<PredicatedResponseDTO> GetAllEmployeesDashboardAsync(string searchKey, int pageindex, int pageSize);
        Task<PredicatedResponseDTO> GetAllEmployeesDashboardFilterAsync(EmployeeSpecParams paginationParams);
        Task<IEnumerable<EmployeeDTO>> GetSelections();
        Task<EmployeeDTO> GetSelection(Guid employeeGuid);

        Task<ResponseDTO> ChangeIsDeletedStatus(Guid employeeId);

        Task<ResponseDTO> GetEmployeeListForReportingManager();

        Task<ResponseDTO> GetFilterMenu();
        Task<ResponseDTO> GetReportFilter();
        Task<bool> CheckIdNumber(string idNumber);
        Task<ResponseDTO> CheckEmailAddressExistence(string email, Guid guid);
        Task<ResponseDTO> CheckPhoneNumberExistence(string phone, Guid guid);

        Task<bool> CheckDeptData(Guid idNumber);
        Task<bool> CheckRole(Guid idNumber);
        Task<bool> CheckCountry(Guid idNumber);
        Task<bool> CheckDutyStation(Guid idNumber);

        Task<ResponseDTO> GetEmployeesCount();
        Task<PredicatedResponseDTO> GetEmployeeAssignments(EmpAssignmentSpecParams specParams);

    }
}
