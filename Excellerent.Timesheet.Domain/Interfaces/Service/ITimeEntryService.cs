using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Service
{
    public interface ITimeEntryService : ICRUD<TmpTimeEntryEntity, TmpTimeEntry>
    {
        Task<ResponseDTO> GetTimeEntry(Guid timeEntryId);
        Task<ResponseDTO> GetTimeEntryForUpdateOrDelete(Guid timeEntryId);
        Task<ResponseDTO> GetTimeEntries(Guid timeSheetId, DateTime? date, Guid? projectId);
        Task<ResponseDTO> AddTimeEntry(Guid employeeId, TmpTimeEntryDto timeEntryDto);
        Task<ResponseDTO> UpdateTimeEntry(TmpTimeEntryDto timeEntryDto);
        Task<ResponseDTO> RequestForApproval(Guid timesheetId);
        Task<ResponseDTO> AddTImeEntryForRangeOfDays(Guid employeeId, TmpTimeEntryDto[] entries);
        Task<ResponseDTO> RemoveTimeEntryById(Guid timeEntryId);
        Task<ResponseDTO> RemoveTimeEntry(TmpTimeEntryDto timeEntryDto);
    }
}
