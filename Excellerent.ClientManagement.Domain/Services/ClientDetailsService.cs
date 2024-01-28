
using Excellerent.ClientManagement.Domain.DTOs;
using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Domain.Services
{
    public class ClientDetailsService : CRUD<ClientDetailsEntity, ClientDetails>, IClientDetailsService
    {
        private readonly IClientDetailsRepository _clientDetailsRepository;
        private readonly IClientContactRepository _clientContactRepository;
        private readonly ICompanyContactRepository _companyContactRepository;
        private readonly IOperatingAddressRepository _operatingAddressRepository;
        private readonly IBillingAddressRepository _billingAddressRepository;
        private readonly IEmployeeService _employeeService;


        public ClientDetailsService(IClientDetailsRepository clientDetailsRepository, IClientContactRepository clientContactRepository,
            ICompanyContactRepository companyContactRepository, IOperatingAddressRepository operatingAddressRepository,
            IBillingAddressRepository billingAddressRepository, IEmployeeService employeeService) : base(clientDetailsRepository)
        {
            _clientDetailsRepository = clientDetailsRepository;
            _clientContactRepository = clientContactRepository;
            _companyContactRepository = companyContactRepository;
            _operatingAddressRepository = operatingAddressRepository;
            _billingAddressRepository = billingAddressRepository;
            _employeeService = employeeService;
        }

        public async Task<ResponseDTO> AddNewClient(ClientDetailsEntity client)
        {
            try
            {
                return new ResponseDTO(ResponseStatus.Success, "Client Data Added successfully",
                    await _clientDetailsRepository.AddAsync(client.MapToModel()));
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Input", null);
            }
        }

        public async Task<ClientDetails> GetClientById(Guid id)
        {
            return (await _clientDetailsRepository.GetClientById(id));

        }

        public async Task<IEnumerable<ClientDetailsEntity>> GetClientByName(string clientName)
        {
            var data = _clientDetailsRepository.GetClientByName(clientName);
            return (await data).Select(c => new ClientDetailsEntity(c));
        }

        public async Task<ResponseDTO> GetClientFullData()
        {
            List<ClientDetailsEntity> ClientEntity = new List<ClientDetailsEntity>();
            var clientData = (await _clientDetailsRepository.GetClientFullData()).Where(x => !x.IsDeleted).Select(p => new ClientDetailsEntity(p)).ToList();
            foreach (var data in clientData)
            {
                try
                {
                    var salesPerson = _employeeService.GetSelection(data.SalesPersonGuid).Result;

                    if (salesPerson != null)
                    {
                        data.SalesPersonName = salesPerson.Name;
                        data.SalesPerson = salesPerson;
                    }
                    data.ClientStatusName = data.ClientStatus.StatusName;
                    data.OperatingAddressCountry = data.OperatingAddress[0].Country;
                    var contacts = data.CompanyContacts.ToList();
                    for (int count = 0; count < contacts.Count(); count++)
                    {
                        data.CompanyContacts[count].Employee = _employeeService.GetSelection(contacts[count].EmployeeGuid).Result;
                    }
                }
                catch (Exception ex) { }

                ClientEntity.Add(data);
            }
            return new ResponseDTO
            {
                Data = ClientEntity,
                Message = "Client full data",
                Ex = null,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<PredicatedResponseDTO> GetPaginatedClient(Guid? id, string searchKey, int? pageIndex, int? pageSize)
        {
            int itemPerPage = pageSize ?? 10;
            int PageIndex = pageIndex ?? 1;
            EmployeeDTO employee;
            var predicate = PredicateBuilder.True<ClientDetails>();
            List<ClientDetailsEntity> clientDetailsEntities = new List<ClientDetailsEntity>();
            if (id != null)
                predicate = predicate.And(p => p.Guid == id);
            else
                predicate = string.IsNullOrEmpty(searchKey) ? null
                           : predicate.And
                            (
                                p => p.ClientName.ToLower().Contains(searchKey.ToLower())



                            );
            var clientData = (await _clientDetailsRepository.GetPaginatedClient(predicate, PageIndex, itemPerPage))
                    .Where(x => !x.IsDeleted && x.ClientName != "Leave").Select(p => new ClientDetailsEntity(p)
                    ).ToList();
            foreach (var data in clientData.ToList())
            {
                try
                {
                    var salesPerson = _employeeService.GetSelection(data.SalesPersonGuid);
                    data.SalesPersonName = salesPerson.Result.Name;
                    data.SalesPerson = salesPerson.Result;
                    data.ClientStatusName = data.ClientStatus.StatusName;
                    if (data.OperatingAddress.Count > 0)
                    {
                        data.OperatingAddressCountry = data.OperatingAddress[0].Country;
                    }
                    var contacts = data.CompanyContacts.ToList();
                    for (int count = 0; count < contacts.Count(); count++)
                    {
                        data.CompanyContacts[count].Employee = _employeeService.GetSelection(contacts[count].EmployeeGuid).Result;
                    }
                }
                catch (Exception ex) { }

                clientDetailsEntities.Add(data);
            }
            int TotalRowCount = await _clientDetailsRepository.CountAsync(x => !x.IsDeleted && x.ClientName != "Internal" && x.ClientName != "Leave");
            return new PredicatedResponseDTO
            {
                Data = clientData,
                TotalRecord = TotalRowCount,
                PageIndex = PageIndex,
                PageSize = itemPerPage,
                TotalPage = TotalRowCount % itemPerPage == 0 ? TotalRowCount / itemPerPage : TotalRowCount / itemPerPage + 1
            };
        }

        public async Task<ResponseDTO> UpdateClient(ClientDetailsEntity client)
        {
            try
            {

                var newContact = client.ClientContacts.FindAll(x => x.Guid == Guid.Empty);
                var newCompany = client.CompanyContacts.FindAll(x => x.Guid == Guid.Empty);
                var newOprAddrs = client.OperatingAddress.FindAll(x => x.Guid == Guid.Empty);
                var newBllAddrs = client.BillingAddress.FindAll(x => x.Guid == Guid.Empty);

                client.ClientContacts.RemoveAll(x => x.Guid == Guid.Empty);
                client.CompanyContacts.RemoveAll(x => x.Guid == Guid.Empty);
                client.OperatingAddress.RemoveAll(x => x.Guid == Guid.Empty);
                client.BillingAddress.RemoveAll(x => x.Guid == Guid.Empty);

                var result = await _clientDetailsRepository.GetClientById(client.Guid);

                var clientModel = client.MapToModel(result);

                foreach (var item in newBllAddrs.ToList())
                {

                    clientModel.BillingAddress.Add(item.MapToModel());

                }
                foreach (var item in newContact.ToList())
                {

                    clientModel.ClientContacts.Add(item.MapToModel());

                }
                foreach (var item in newCompany.ToList())
                {

                    clientModel.CompanyContacts.Add(item.MapToModel());

                }
                foreach (var item in newOprAddrs.ToList())
                {

                    clientModel.OperatingAddress.Add(item.MapToModel());

                }
                await _clientDetailsRepository.UpdateAsync(clientModel);
                return new ResponseDTO(ResponseStatus.Success, "Client Data Updated successfully", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Input", null);
            }

        }
        public async Task<ResponseDTO> DeleteClient(Guid id, bool ischeck)
        {
            try
            {

                var result = await _clientDetailsRepository.FindOneAsyncForDelete(c => c.Guid == id);
                if (result == null)
                {
                    return new ResponseDTO
                    {
                        ResponseStatus = ResponseStatus.Error,
                        Message = "There is no client with the given id!",
                        Data = null,
                    };
                }
                var projects = await _clientDetailsRepository.GetProjectByClientId(result.Guid);

                if (projects.Data != null)
                {
                    return new ResponseDTO
                    {
                        ResponseStatus = ResponseStatus.Error,
                        Message = "Client was not Deleted. It has one or more projects.",
                        Data = null,
                    };
                }
                else
                {
                    if (ischeck)
                    {
                        return new ResponseDTO
                        {
                            ResponseStatus = ResponseStatus.Success,
                            Message = "the client can'be deleted",
                            Data = null,
                        };
                    }


                    await _clientDetailsRepository.DeleteAsync(result);
                    return new ResponseDTO(ResponseStatus.Success, "Client Data Deleted successfully", null);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, "Invalid Operation", null);

            }
        }

        public async Task<bool> IsEmployeeSalesPerson(Guid id)
        {
            int e = (await _clientDetailsRepository.FindAsync(x => x.SalesPersonGuid == id)).Count();
            return e > 0 ? true : false;

        }
    }
}