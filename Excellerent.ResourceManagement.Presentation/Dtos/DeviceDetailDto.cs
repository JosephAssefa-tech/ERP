using Excellerent.ResourceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Dtos
{
    public class DeviceDetailDto
    {
        [Required]
        public Guid CategoryGuid { get; set; }
        [Required]
        public Guid SubCategoryGuid { get; set; }
        public string CompanyDeviceCode { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string BusinessUnit { get; set; }
        [Required]
        public string IsWorking { get; set; }
        public string AllocateTo { get; set; }
        [Required]
        public Guid DeviceClassificationGuid { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        public string Purchaser { get; set; }
        public string InvoiceNumber { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Warranty { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        [Required]
        public string Notes { get; set; }

        public DeviceDetailsEntity MapToEntity()
        {
            DeviceDetailsEntity e = new DeviceDetailsEntity();
            e.CategoryGuid = this.CategoryGuid;
            e.SubCategoryGuid = this.SubCategoryGuid;
            e.CompanyDeviceCode = this.CompanyDeviceCode;
            e.DeviceName = this.DeviceName;
            e.BusinessUnit = this.BusinessUnit;
            e.IsWorking = this.IsWorking;
            e.AllocateTo = this.AllocateTo;
            e.DeviceClassificationGuid = this.DeviceClassificationGuid;
            e.PurchaseDate = this.PurchaseDate;
            e.Purchaser = this.Purchaser;
            e.InvoiceNumber = this.InvoiceNumber;
            e.Manufacturer = this.Manufacturer;
            e.SerialNumber = this.SerialNumber;
            e.Warranty = this.Warranty;
            e.WarrantyEndDate = this.WarrantyEndDate;
            e.Notes = this.Notes;
            return e;
        }
    }
}
