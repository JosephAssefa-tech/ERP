using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class FamilyDetailService : CRUD<FamilyDetailsEntity, FamilyDetails>, IFamilyDetailService
    {
        private readonly IFamilyDetailRepository _familyDetailRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public FamilyDetailService(IFamilyDetailRepository familyDetailRepository, IEmployeeRepository employeeRepository) : base(familyDetailRepository)
        {
            _familyDetailRepository = familyDetailRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> DeleteFamilyMember(Guid id)
        {
            var member = _familyDetailRepository.FindOneAsyncForDelete(x=>x.Guid == id);
            await _familyDetailRepository.DeleteAsync(member.Result);
            return true;
        }

        public async Task<IEnumerable<FamilyDetails>> GetFamilyDetailByEmployeeId(Guid EmployeeId)
        {
            return await _familyDetailRepository.GetFamilyDetailByEmployeeId(EmployeeId);
        }

    }
}
