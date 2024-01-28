using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class DeviceClassificationService : IDeviceClassificationService
    {
        List<DeviceClassification> deviceClassifications = new List<DeviceClassification>();
        public DeviceClassificationService()
        {
            deviceClassifications.AddRange(
                new List<DeviceClassification> {
                    new DeviceClassification {
                        Guid = new Guid("207d583d-9063-40ab-a311-5fb1d9583e69"),
                        Name = "Business Unit",
                        Description ="business unit desc",
                    },
                    new DeviceClassification {
                        Guid = new Guid("727acac1-9b19-437d-ab0a-5da60d516382"),
                        Name = "Department",
                        Description = "department desc"
                    },
                    new DeviceClassification {
                        Guid = new Guid("17c95ca9-fc71-4f13-922d-5accbdf89a63"),
                        Name = "Employee",
                        Description = "employee desc"
                    }
                }
            );
        }
        public List<DeviceClassification> GetDeviceClassifications()
        {
            return deviceClassifications;
        }

        public List<DeviceClassification> GetDeviceClassificationById(Guid id)
        {
            return id == null ? deviceClassifications : deviceClassifications.Where(x => x.Guid == id).ToList();
        }
    }
}
