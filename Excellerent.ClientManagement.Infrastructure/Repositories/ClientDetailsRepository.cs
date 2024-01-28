using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class ClientDetailsRepository : AsyncRepository<ClientDetails>, IClientDetailsRepository
    {
        private readonly EPPContext _context;

        public ClientDetailsRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientDetails> GetClientById(Guid id)
        {
            try
            {
                return (await _context.ClientDetails.Where(c=>c.Guid.Equals(id))
                    .Include(o=>o.OperatingAddress)
                    .Include(c => c.ClientContacts)
                    .Include(b => b.BillingAddress)
                    .Include(c => c.CompanyContacts)
                    .FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<ClientDetails>> GetClientByName(string clientName)
        {
            try
            {
                IEnumerable<ClientDetails> clientDetails = (await base.GetQueryAsync(x => x.ClientName == clientName)).ToList();
                return clientDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDetails>> GetClientFullData()
        {
            try
            {
                return await _context.ClientDetails.Include(x => x.OperatingAddress)
                                        .Include(x => x.BillingAddress)
                                        .Include(x => x.ClientContacts)
                                         .Include(x => x.ClientStatus)

                                        .Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDetails>> GetPaginatedClient(Expression<Func<ClientDetails, bool>> predicate, int pageIndex, int pageSize)
        {
            return predicate == null ? (await _context.ClientDetails.OrderByDescending(x => x.CreatedDate).Where(x => !x.IsDeleted && x.ClientName != "Internal").Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(x => x.ClientStatus)
                .Include(x => x.OperatingAddress)
                .Include(x => x.BillingAddress)
                .Include(x => x.ClientContacts)
                .Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync())
         : (await _context.ClientDetails.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize)
         .Include(x => x.ClientStatus)
         .Include(x => x.OperatingAddress)
         .Include(x => x.BillingAddress)
         .Include(x => x.ClientContacts)
         .Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync());
        }

        public async Task<ResponseDTO> GetProjectByClientId(Guid id)
        {
            if (_context.Project.Count() == 0)
            {
                return new ResponseDTO(ResponseStatus.Success, " ", null);
            }
            else
            {
                var check = await _context.Project?.Where(x => x.ClientGuid.Equals(id) && x.IsDeleted.Equals(false)).ToListAsync();
                return new ResponseDTO(ResponseStatus.Success, " ", check.Count()==0?null:check);
            }
        }

        public ClientDetails UpdateClient(ClientDetails client)
        {
            var existingClient = _context.ClientDetails
               .Include(x => x.ClientContacts)
               .Include(x => x.CompanyContacts)
               .Include(x => x.OperatingAddress)
               .Include(x => x.BillingAddress)
               .FirstOrDefault(x => x.Guid.Equals(client.Guid));

            if (existingClient == null)
                return null;
            else
            {
                existingClient.ClientName = client.ClientName;
                existingClient.ClientStatus = client.ClientStatus;
                existingClient.Description = client.Description;
                existingClient.ClientStatusGuid = client.ClientStatusGuid;
                existingClient.SalesPersonGuid = client.SalesPersonGuid;
                existingClient.ClientName = client.ClientName;

                if (existingClient.ClientContacts.Count() > 0 && client.ClientContacts.Count() > 0)
                {
                    for (int i = 0; i < client.ClientContacts.Count(); i++)
                    {
                        existingClient.ClientContacts[i].ContactPersonName = client.ClientContacts[i].ContactPersonName;
                        existingClient.ClientContacts[i].Email = client.ClientContacts[i].Email;
                        existingClient.ClientContacts[i].PhoneNumber = client.ClientContacts[i].PhoneNumber;
                        existingClient.ClientContacts[i].PhoneNumberPrefix = client.ClientContacts[i].PhoneNumberPrefix;
                    }
                }

                if (existingClient.CompanyContacts != null && client.CompanyContacts != null)
                {
                    for (int i = 0; i < client.CompanyContacts.Count(); i++)
                    {
                        existingClient.CompanyContacts[i].EmployeeGuid = client.CompanyContacts[i].EmployeeGuid;

                    }
                }

                if (existingClient.OperatingAddress != null)
                {
                    for (int i = 0; i < client.OperatingAddress.Count(); i++)
                    {
                        existingClient.OperatingAddress[i].Country = client.OperatingAddress[i].Country;
                        existingClient.OperatingAddress[i].City = client.OperatingAddress[i].City;
                        existingClient.OperatingAddress[i].Address = client.OperatingAddress[i].Address;
                        existingClient.OperatingAddress[i].State = client.OperatingAddress[i].State;
                        existingClient.OperatingAddress[i].ZipCode = client.OperatingAddress[i].ZipCode;

                    }
                }


                if (existingClient.BillingAddress.Count() > 0 && client.BillingAddress.Count() > 0)
                {
                    for (int i = 0; i < client.BillingAddress.Count(); i++)
                    {
                        existingClient.BillingAddress[i].Name = client.BillingAddress[i].Name;
                        existingClient.BillingAddress[i].Address = client.BillingAddress[i].Address;
                        existingClient.BillingAddress[i].Affliation = client.BillingAddress[i].Affliation;
                        existingClient.BillingAddress[i].ZipCode = client.BillingAddress[i].ZipCode;
                        existingClient.BillingAddress[i].Country = client.BillingAddress[i].Country;
                        existingClient.BillingAddress[i].State = client.BillingAddress[i].State;
                        existingClient.BillingAddress[i].City = client.BillingAddress[i].City;

                    }
                }

                _context.SaveChangesAsync(true);

                return client;
            }
        }


    }
}