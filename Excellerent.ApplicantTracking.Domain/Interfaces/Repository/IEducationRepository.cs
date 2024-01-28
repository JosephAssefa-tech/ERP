using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Interfaces.Repository
{
    public interface IEducationRepository : IAsyncRepository<Education>
    {
        Task<IEnumerable<EducationEntity>> GetWithPredicate(Expression<Func<Education, bool>> predicate);
    }
}
