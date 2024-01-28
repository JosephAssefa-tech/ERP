using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Services
{
    public class CompanyContactService : CRUD<CompanyContactEntity, CompanyContact>, ICompanyContactService
    {
        private readonly ICompanyContactRepository _repository;

        public CompanyContactService(ICompanyContactRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO> DeleteCompanyContact(Guid id)
        {
            try
            {
                var result = await _repository.FindOneAsync(x => x.Guid.Equals(id));
                await _repository.DeleteAsync(result);
                return new ResponseDTO(ResponseStatus.Success, "Company Contact Data Deleted successfully", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Input", null);

            }
        }
    }
}