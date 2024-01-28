using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Services
{
    public class OperatingAddressService : CRUD<OperatingAddressEntity, OperatingAddress>, IOperatingAddressService
    {
        private readonly IOperatingAddressRepository _repository;

        public OperatingAddressService(IOperatingAddressRepository repository): base(repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO> DeleteOperatingAddress(Guid id)
        {
            try
            {
                var result = await _repository.FindOneAsync(x => x.Guid.Equals(id));
                await _repository.DeleteAsync(result);
                return new ResponseDTO(ResponseStatus.Success, "Operating Address Data Deleted successfully", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Input", null);

            }
        }
    }
}