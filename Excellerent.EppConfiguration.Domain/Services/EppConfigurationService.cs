using Excellerent.EppConfiguration.Domain.Entities;
using Excellerent.EppConfiguration.Domain.Interfaces.Repository;
using Excellerent.EppConfiguration.Domain.Interfaces.Service;
using Excellerent.EppConfiguration.Domain.Mapping;
using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Domain.Services
{
    public class EppConfigurationService : CRUD<ConfigurationEntity, Configuration>, IEppConfigurationService
    {
        private readonly IEppConfigurationRepository _eppConfigurationRepository;

        public EppConfigurationService(IEppConfigurationRepository eppConfigurationRepository) : base(eppConfigurationRepository)
        {
            _eppConfigurationRepository = eppConfigurationRepository;
        }

        public async Task<ResponseDTO> GetEppConfiguration()
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                List<Configuration> configurations = (await _eppConfigurationRepository.GetEppConfiguration()).ToList();
                List<ConfigurationEntity> configurationEntities = configurations.Select(config => new ConfigurationEntity(config)).ToList();

                response.ResponseStatus = ResponseStatus.Success;
                response.Message = "Epp Configuration";
                response.Ex = null;
                response.Data = configurationEntities.MapToDto();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.Error;
                response.Message = ex.Message;
                response.Ex = ex;
                response.Data = null;
            }

            return response;
        }

        public async Task<ResponseDTO> AddEppConfiguration(IEnumerable<ConfigurationEntity> configurationEntities)
        {
            bool result = false;
            ResponseDTO response = new ResponseDTO();

            try
            {
                result = await _eppConfigurationRepository.AddEppConfiguration(configurationEntities.Select(eppc => eppc.MapToModel()));

                response.ResponseStatus = ResponseStatus.Success;
                response.Message = "Epp Configuration added/updated successfuly";
                response.Data = null;
                response.Ex = null;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.Error;
                response.Message = ex.Message;
                response.Data = null;
                response.Ex = ex;
            }

            return response;
        }
    }
}
