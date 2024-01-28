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
    public interface ITimeSheetService : ICRUD<TimeSheetEntity, TimeSheet>
    {        
        // Get Timesheet by Timesheet Id
        Task<ResponseDTO> GetTimeSheet(Guid id, bool requestedForApproval = false);
        Task<TimeSheet> GetTimeSheetById(Guid id);
        // Get Timesheet by Employee Id and Date
        Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime? date);
        // Get Timesheet by Employee Id, fromDate, and toDate
        Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime fromDate, DateTime toDate);

        Task<ResponseDTO> GetTimeSheetsForReport(Guid? clientId, List<Guid> projectIds, DateTime fromDate, DateTime toDate);

        // Add Timesheet
        Task<ResponseDTO> AddTimeSheet(Guid employeeId, TmpTimeEntryDto timeEntryDto);
        Task<ResponseDTO> AddTimeSheet(TimeSheetDto timeSheetDto, TmpTimeEntryDto timeEntryDto);

        Task SyncRequestedForApprovalTimeEntries(Guid timesheetId);

        Task SyncApprovedTimeEntries(Guid timesheetId);

        Task<bool> Update(TimeSheet timesheet);
        Task<bool> IsEmployeeWithTimeesheetExists(Guid employeeGui);
        Task<ResponseDTO> GetTimeSheetAgregateReport(Guid? clientId, List<Guid> projectGuids, DateTime? dateFrom, DateTime? dateTo);
    }
}
