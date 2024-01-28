using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.Services.Helpers;
using Excellerent.SharedModules.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Excellerent.ProjectManagement.Infrastructure.Specificationes
{
    public class GetProjectsPaginationSpec : ISpecification<Project>
    {
        private readonly PaginationParams _paginationParams;
        private readonly Guid _userGuid;
        private readonly Guid _employeeGuid;
        private readonly List<Guid> _userAssignedprojectGuid;
        private readonly Boolean _isProjectAdmin;
        public GetProjectsPaginationSpec(PaginationParams paginationParams, Guid userGuid,Guid employeeGuid,
            List<Guid> userAssignedprojectGuid, Boolean isProjectAdmin)
        {
          _paginationParams= paginationParams;
            _userGuid = userGuid;
            _employeeGuid = employeeGuid;
            _userAssignedprojectGuid= userAssignedprojectGuid;
            _isProjectAdmin= isProjectAdmin;
        }
        public IQueryable<Project> SatisfyingEntitiesFrom(IQueryable<Project> query)
        {

            query = query.Include(p=>p.ProjectStatus).Include(p=>p.Client).Include(p => p.Supervisor).Where(p=>p.IsDeleted==false).AsQueryable();
                   if (_isProjectAdmin == false)
                query = query.Where(p => p.SupervisorGuid == _employeeGuid || p.CreatedbyUserGuid==_userGuid || 
                     _userAssignedprojectGuid.Contains(p.Guid)).AsQueryable();

            if (_paginationParams.client != null)
                query = query.Where(p => _paginationParams.client.Contains(p.ClientGuid.ToString()));  
        
  
                  if(_paginationParams.status!=null)
                    query = query.Where(p => _paginationParams.status.Contains(p.ProjectStatusGuid.ToString()));
            
             if(_paginationParams.supervisorId!=null)
                    query = query.Where(p => _paginationParams.supervisorId.Contains(p.SupervisorGuid));
                   

            if(_paginationParams.searchKey!=null)
                query =query.Where(p=>p.ProjectName.ToLower().Trim().Contains(_paginationParams.searchKey.ToLower().Trim()));


           
            return query;
         
        }

        public Project SatisfyingEntityFrom(IQueryable<Project> query)
        {
            throw new NotImplementedException();
        }
    }
}
