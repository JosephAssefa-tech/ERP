using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeSheetConfigService : CRUD<ConfigurationEntity, Configuration>, ITimeSheetConfigService
    {
        private readonly ITimeSheetConfigRepository _timeSheetConfigRepository;

        public TimeSheetConfigService(ITimeSheetConfigRepository repository) : base(repository)
        {
            _timeSheetConfigRepository = repository;
        }

        public async Task<ResponseDTO> GetTimeSheetConfiguration()
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                List<Configuration> configurations = (await _timeSheetConfigRepository.GetTimeSheetConfiguration()).ToList();
                List<ConfigurationEntity> configurationEntities = configurations.Select(config => new ConfigurationEntity(config)).ToList();

                response.ResponseStatus = ResponseStatus.Success;
                response.Message = "TimeSheet Configuration";
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

        public async Task<ResponseDTO> AddTimeSheetConfiguration(IEnumerable<ConfigurationEntity> configurationEntities)
        {
            bool result = false;
            ResponseDTO response = new ResponseDTO();

            try
            {
                result = await _timeSheetConfigRepository.AddTimeSheetConfiguration(configurationEntities.Select(tsc => tsc.MapToModel()));

                response.ResponseStatus = ResponseStatus.Success;
                response.Message = "Timesheet configuration added/updated successfuly";
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

            return new ResponseDTO(ResponseStatus.Success, "Timesheet configuration added/Updated successfuly", null);
        }
    }
}
