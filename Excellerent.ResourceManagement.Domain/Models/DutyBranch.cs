
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class DutyBranch : BaseAuditModel
    {
        [Required]
        [ForeignKey("Country")]
        public Guid CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
