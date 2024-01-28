using Excellerent.EppConfiguration.Domain.Entities;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Interfaces.Service
{
    public interface IEppConfigurationService : ICRUD<ConfigurationEntity, Configuration>
    {
        Task<ResponseDTO> GetEppConfiguration();

        Task<ResponseDTO> AddEppConfiguration(IEnumerable<ConfigurationEntity> configurationEntities);
    }
}
