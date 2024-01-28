using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class DeviceClassificationEntity : BaseEntity<DeviceClassification>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override DeviceClassification MapToModel()
        {
            DeviceClassification a = new DeviceClassification();
            a.Name = Name;
            a.Description = Description;
            return a;
        }

        public override DeviceClassification MapToModel(DeviceClassification t)
        {
            DeviceClassification a = t;
            a.Name = Name;
            a.Description = Description;
            return a;
        }
    }
}
