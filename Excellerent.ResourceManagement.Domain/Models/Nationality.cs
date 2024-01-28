using Excellerent.SharedModules.Seed;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class Nationality : BaseAuditModel
    {
        [Key]
        public override Guid Guid { get; set; }

        public string Name { get; set; }
    }
}
