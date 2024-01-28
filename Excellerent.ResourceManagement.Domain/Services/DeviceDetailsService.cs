using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using Excellerent.SharedModules.DTO;
using LinqKit;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class DeviceDetailsService : CRUD<DeviceDetailsEntity, DeviceDetails>, IDeviceDetailsService
    {
        private readonly IDeviceDetailsRepository _deviceDetailsRepository;
        public DeviceDetailsService(IDeviceDetailsRepository repo) : base(repo)
        {
            _deviceDetailsRepository = repo;

        }

        public async Task<ResponseDTO> Get(Guid id)
        {
            DeviceDetails m = await _deviceDetailsRepository.Get(id);
            DeviceDetailsEntity d = new DeviceDetailsEntity(m);
            return new ResponseDTO
            {
                Data = d,
                ResponseStatus = ResponseStatus.Success
            };
        }

        public async Task<DeviceDetails> FindOneAsyncForDelete(Guid id)
        {
            return await _deviceDetailsRepository.FindOneAsyncForDelete(d => d.Guid == id);
        }

        public async Task<PredicatedResponseDTO> GetWithPredicate(Guid? id, string searchKey, int? pageIndex, int? pageSize)
        {
            int ItemsPerPage = pageSize ?? 10;
            int PageIndex = pageIndex ?? 1;
            var predicate = PredicateBuilder.True<DeviceDetails>();

            if (id != null)
                predicate = predicate.And(p => p.Guid == id);
            else
                predicate = string.IsNullOrEmpty(searchKey) ? null
                           : predicate.And
                            (
                                p => p.DeviceName.ToLower().Contains(searchKey.ToLower())
                            );
            var deviceDetails = (await _deviceDetailsRepository.GetWithPredicateAsync(predicate, PageIndex, ItemsPerPage))
                    .Select(d => new DeviceDetailsEntity(d)).ToList();
            
            int TotalRowCount = await _deviceDetailsRepository.CountAsync();
            return new PredicatedResponseDTO
            {
                Data = deviceDetails,
                TotalRecord = TotalRowCount,
                PageIndex = PageIndex,
                PageSize = ItemsPerPage,
                TotalPage = TotalRowCount % ItemsPerPage == 0 ? TotalRowCount / ItemsPerPage : TotalRowCount / ItemsPerPage + 1
            };

        }
    }
}
