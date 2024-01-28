using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Excellerent.ProjectManagement.Infrastructure.Specificationes
{
    public class GetDashboardProjectSpec : ISpecification<Project>
    {

        private readonly  ProjectType? projectType;
        public GetDashboardProjectSpec( ProjectType? projectType)
        {
            this.projectType = projectType;
        }
      
        public IQueryable<Project> SatisfyingEntitiesFrom(IQueryable<Project> query)
        {
            query = query.Include(p => p.ProjectStatus).Include(p => p.Client)
            .Where(p => p.IsDeleted == false  && p.ProjectStatus.StatusName=="Active" &&
            p.ProjectName!= "Leave" && p.Client.ClientName!="Leave").AsQueryable();

            if (this.projectType != null)
                query = query.Where(p=>p.ProjectType == this.projectType);

            return query;
        }

        public Project SatisfyingEntityFrom(IQueryable<Project> query)
        {

            throw new NotImplementedException();
        }
    }
}


