using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class DeviceDetailsEntity : BaseEntity<DeviceDetails>
    {
        [Required]
        public Guid CategoryGuid { get; set; }
        // public Category Category { get; set; }
        [Required]
        public Guid SubCategoryGuid { get; set; }
        // public SubCategory SubCategory { get; set; }
        [Required]
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
        // public DeviceClassification DeviceClassification { get; set; }
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
        public DeviceDetailsEntity()
        {

        }

        public DeviceDetailsEntity(DeviceDetails t) : base(t)
        {
            CategoryGuid = t.CategoryGuid;
            // Category = t.Category;
            SubCategoryGuid = t.SubCategoryGuid;
            // SubCategory = t.SubCategory;
            CompanyDeviceCode = t.CompanyDeviceCode;
            DeviceName = t.DeviceName;
            BusinessUnit = t.BusinessUnit;
            IsWorking = t.IsWorking;
            AllocateTo = t.AllocateTo;
            DeviceClassificationGuid = t.DeviceClassificationGuid;
            // DeviceClassification = t.DeviceClassification;
            PurchaseDate = t.PurchaseDate;
            Purchaser = t.Purchaser;
            InvoiceNumber = t.InvoiceNumber;
            Manufacturer = t.Manufacturer;
            SerialNumber = t.SerialNumber;
            Warranty = t.Warranty;
            WarrantyEndDate = t.WarrantyEndDate;
            Notes = t.Notes;
        }

        public override DeviceDetails MapToModel()
        {
            DeviceDetails a = new DeviceDetails();
            a.Guid = Guid;
            a.CategoryGuid = CategoryGuid;
            // a.Category = Category;
            a.SubCategoryGuid = SubCategoryGuid;
            // a.SubCategory = SubCategory;
            a.CompanyDeviceCode = CompanyDeviceCode;
            a.DeviceName = DeviceName;
            a.BusinessUnit = BusinessUnit;
            a.IsWorking = IsWorking;
            a.AllocateTo = AllocateTo;
            a.DeviceClassificationGuid = DeviceClassificationGuid;
            // a.DeviceClassification = DeviceClassification;
            a.PurchaseDate = PurchaseDate;
            a.Purchaser = Purchaser;
            a.InvoiceNumber = InvoiceNumber;
            a.Manufacturer = Manufacturer;
            a.SerialNumber = SerialNumber;
            a.Warranty = Warranty;
            a.WarrantyEndDate = WarrantyEndDate;
            a.Notes = Notes;
            return a;
        }

        public override DeviceDetails MapToModel(DeviceDetails t)
        {
            DeviceDetails a = t;
            a.Guid = Guid;
            a.CategoryGuid = CategoryGuid;
            // a.Category = Category;
            a.SubCategoryGuid = SubCategoryGuid;
            // a.SubCategory = SubCategory;
            a.CompanyDeviceCode = CompanyDeviceCode;
            a.DeviceName = DeviceName;
            a.BusinessUnit = BusinessUnit;
            a.IsWorking = IsWorking;
            a.AllocateTo = AllocateTo;
            a.DeviceClassificationGuid = DeviceClassificationGuid;
            // a.DeviceClassification = DeviceClassification;
            a.PurchaseDate = PurchaseDate;
            a.Purchaser = Purchaser;
            a.InvoiceNumber = InvoiceNumber;
            a.Manufacturer = Manufacturer;
            a.SerialNumber = SerialNumber;
            a.Warranty = Warranty;
            a.WarrantyEndDate = WarrantyEndDate;
            a.Notes = Notes;
            return a;
        }
    }
}
