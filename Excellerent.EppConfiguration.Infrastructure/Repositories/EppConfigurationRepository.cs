using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Infrastructure.Repositories
{
    public class EppConfigurationRepository : AsyncRepository<Configuration>, IEppConfigurationRepository
    {
        public EppConfigurationRepository(EPPContext context) : base(context)
        { }

        public async Task<IEnumerable<Configuration>> GetEppConfiguration()
        {
            return await GetAllAsync();
        }

        public async Task<bool> AddEppConfiguration(IEnumerable<Configuration> configurations)
        {
            foreach (Configuration eppConfig in configurations)
            {
                var tmpEppConfig = await FindOneAsyncForDelete((tsc => tsc.Key == eppConfig.Key));
                await SaveConfiguration(eppConfig, tmpEppConfig);
            }

            return true;
        }

        private async Task SaveConfiguration(Configuration eppConfig, Configuration tmpEppConfig)
        {
            if (tmpEppConfig == null)
            {
                await AddAsync(eppConfig);
            }
            else
            {
                eppConfig.Guid = tmpEppConfig.Guid;
                await UpdateAsync(eppConfig);
            }
        }
    }
}
