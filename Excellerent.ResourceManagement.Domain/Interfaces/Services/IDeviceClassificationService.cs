using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface IDeviceClassificationService
    {
        public List<DeviceClassification> GetDeviceClassifications();
        public List<DeviceClassification> GetDeviceClassificationById(Guid id);
    }
}
