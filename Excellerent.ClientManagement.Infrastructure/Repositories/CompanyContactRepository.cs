using Excellerent.ClientManagement.Domain.Interfaces;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class CompanyContactRepository : AsyncRepository<CompanyContact>, ICompanyContactRepository
    {
        private readonly EPPContext _context;

        public CompanyContactRepository(EPPContext context) : base(context)
        {
            _context = context;
        }
    }
}