using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class DeviceClassification : BaseAuditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
