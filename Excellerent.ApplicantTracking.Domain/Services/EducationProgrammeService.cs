using Excellerent.ApplicantTracking.Domain.Entities;
using Excellerent.ApplicantTracking.Domain.Interfaces.Repository;
using Excellerent.ApplicantTracking.Domain.Interfaces.Service;
using Excellerent.ApplicantTracking.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ApplicantTracking.Domain.Services
{
    public class EducationProgrammeService : CRUD<EducationProgrammeEntity, LUEducationProgram>, IEducationProgrammeService
    {
        private readonly IEducationProgrammeRepository _repository;
        public EducationProgrammeService(IEducationProgrammeRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<LUEducationProgram> GetByIdAsync(Guid educationProgrammId)
        {
            var programme = (await _repository.GetQueryAsync(x => x.Guid == educationProgrammId)).FirstOrDefault();
            return programme;
        }
        public new  async Task<IEnumerable<EducationProgrammeEntity>> GetAll()
        {
            var models = await base.GetAll();
            return models?.Select(x => new EducationProgrammeEntity(x));
        }
        public async Task<ResponseDTO> AddAsync(EducationProgrammeEntity entity)
        {
            var model = await _repository.AddAsync(entity.MapToModel());
            return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
        }
    }
}
