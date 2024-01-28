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
    public class ClientContactService : CRUD<ClientContactEntity, ClientContact>, IClientContactService
    {
        private readonly IClientContactRepository _clientContactRepository;


        public ClientContactService(IClientContactRepository clientContactRepository):base(clientContactRepository)
        {
            _clientContactRepository = clientContactRepository;
        }

        public async Task<ResponseDTO> DeleteClientContact(Guid id)
        {
            try
            {
                var result = await _clientContactRepository.FindOneAsync(x=>x.Guid.Equals(id));

                await _clientContactRepository.DeleteAsync(result);
                return new ResponseDTO(ResponseStatus.Success, "Client Contact Data Deleted successfully", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Input", null);

            }
        }
    }
}