using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Infrastructure.Repositories
{
    public class EducationRepository : AsyncRepository<Education>, IEducationRepository
    {
        EPPContext _ePPContext;
        public EducationRepository(EPPContext eppContext): base(eppContext)
        {
            _ePPContext = eppContext;
        }
         
        public async Task<IEnumerable<EducationEntity>> GetWithPredicate(Expression<Func<Education, bool>> predicate)
        {
            var model = predicate == null ?
                
                (await _ePPContext.Educations
                .Include(x => x.FieldOfStudyId)
                .Include(x => x.EducationProgramme).ToListAsync())
                
                : (await _ePPContext.Educations
                .Include(x => x.FieldOfStudy)
                .Include(x => x.EducationProgramme)
                .Where(predicate).ToListAsync());
            return model.Select(x => new EducationEntity(x));
        }

    }
}
