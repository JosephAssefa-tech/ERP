using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Infrastructure.Repositories
{
   public class FamilyDetailRepository:AsyncRepository<FamilyDetails>, IFamilyDetailRepository
    {
        private readonly EPPContext _context;
    public FamilyDetailRepository(EPPContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<FamilyDetails>> GetFamilyDetailByEmployeeId(Guid id)
    {
        try
        {
            IEnumerable<FamilyDetails> familyDetail = (await base.GetQueryAsync(x => x.EmployeeId == id)).ToList();
            return familyDetail;
        }
        catch (Exception)
        {

            throw;
        }
    }
   
    }
}
