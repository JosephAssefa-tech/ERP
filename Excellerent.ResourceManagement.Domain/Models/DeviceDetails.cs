using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Models
{
    public class DeviceDetails : BaseAuditModel
    {
        public override Guid Guid { get; set; }
        public Guid CategoryGuid { get; set; }
        // public Category Category { get; set; }
        public Guid SubCategoryGuid { get; set; }
        // public SubCategory SubCategory { get; set; }
        public string CompanyDeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string BusinessUnit { get; set; }
        public string IsWorking { get; set; }
        public string AllocateTo { get; set; }
        public Guid DeviceClassificationGuid { get; set; }
        // public DeviceClassification DeviceClassification { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Purchaser { get; set; }
        public string InvoiceNumber { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public string Warranty { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public string Notes { get; set; }
    }
}
