using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Repository
{
    public interface IFamilyDetailRepository : IAsyncRepository<FamilyDetails> 
    {
        Task<IEnumerable<FamilyDetails>> GetFamilyDetailByEmployeeId(Guid EmployeeId);
        
    }
}