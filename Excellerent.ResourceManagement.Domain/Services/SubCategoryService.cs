using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Excellerent.ResourceManagement.Domain.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        List<SubCategory> subCategories = new List<SubCategory>();
        public SubCategoryService()
        {
            subCategories.AddRange(
                new List<SubCategory> {
                    new SubCategory {
                        Guid = new Guid("12acf733-a254-4851-9fa1-5015679e42a0"),
                        CategoryGuid = new Guid("6517eca0-5778-4a39-9060-1d9decfa1e8c"),
                        Name = "Laptop",
                        Description ="laptop desc",
                    },
                    new SubCategory {
                        Guid = new Guid("d1b2d722-9eb5-4f20-9ce9-b03e23263894"),
                        CategoryGuid = new Guid("6517eca0-5778-4a39-9060-1d9decfa1e8c"),
                        Name = "System Unit",
                        Description = "system unit desc"
                    },
                    new SubCategory {
                        Guid = new Guid("d0f50a45-e31f-4be0-b289-4cd4ec3afd5b"),
                        CategoryGuid = new Guid("6517eca0-5778-4a39-9060-1d9decfa1e8c"),
                        Name = "Monitor",
                        Description = "monitor desc"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("b2e99d97-8cd5-4d11-9b7c-94aea5564a28"),
                        CategoryGuid = new Guid("04e27fb5-ea03-4d05-a20b-cc30436bf3e3"),
                        Name = "Apple",
                        Description = "apple desc"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("542b4fe5-136c-4c2e-8ec6-5bfed2ffcb21"),
                        CategoryGuid = new Guid("04e27fb5-ea03-4d05-a20b-cc30436bf3e3"),
                        Name = "Samsung",
                        Description = "samsung desc"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("a64c49e6-e7ac-4c44-a1cd-8ccd904d735b"),
                        CategoryGuid = new Guid("04e27fb5-ea03-4d05-a20b-cc30436bf3e3"),
                        Name = "Huawei",
                        Description = "huawei desc"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("5b1ef5bf-036e-4124-a04f-8d805b02818c"),
                        CategoryGuid = new Guid("23c29d5a-a2f0-440e-beb3-b7b621ba1628"),
                        Name = "HP Printer",
                        Description = "hp printer"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("b2b5605e-5431-400a-9bf6-548b933e70da"),
                        CategoryGuid = new Guid("23c29d5a-a2f0-440e-beb3-b7b621ba1628"),
                        Name = "Canon Printer",
                        Description = "canon printer"
                    },
                    new SubCategory
                    {
                        Guid = new Guid("7a6fd230-a0cd-48ef-847c-0e9c352a4a93"),
                        CategoryGuid = new Guid("23c29d5a-a2f0-440e-beb3-b7b621ba1628"),
                        Name = "Epson Printer",
                        Description = "epson printer"
                    }
                }
            );
        }
        public List<SubCategory> GetSubCategories()
        {
            return subCategories;
        }

        public List<SubCategory> GetByCategoryId(Guid id)
        {
            return id == null ? subCategories : subCategories.Where(x => x.CategoryGuid == id).ToList();
        }
    }
}
