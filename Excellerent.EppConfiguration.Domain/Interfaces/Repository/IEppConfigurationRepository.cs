using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Interfaces.Repository
{
    public interface IEppConfigurationRepository : IAsyncRepository<Configuration>
    {
        Task<IEnumerable<Configuration>> GetEppConfiguration();

        Task<bool> AddEppConfiguration(IEnumerable<Configuration> configurations);
    }
}
