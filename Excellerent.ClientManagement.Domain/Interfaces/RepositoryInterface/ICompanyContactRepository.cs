using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ClientManagement.Domain.Interfaces
{
    public interface ICompanyContactRepository : IAsyncRepository<CompanyContact>
    {
    }
}