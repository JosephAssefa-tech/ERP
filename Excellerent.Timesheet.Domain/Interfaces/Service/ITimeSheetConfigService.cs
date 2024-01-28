using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Service
{
    public interface ITimeSheetConfigService : ICRUD<ConfigurationEntity, Configuration>
    {
        Task<ResponseDTO> GetTimeSheetConfiguration();

        Task<ResponseDTO> AddTimeSheetConfiguration(IEnumerable<ConfigurationEntity> configurationEntities);
    }
}
