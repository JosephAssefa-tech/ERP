using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        List<Category> categories = new List<Category>();
        public CategoryService()
        {
            categories.AddRange(
                new List<Category> {
                    new Category {
                        Guid = new Guid("6517eca0-5778-4a39-9060-1d9decfa1e8c"),
                        Name = "Computer",
                        Description ="device desc",
                    },
                    new Category {
                        Guid = new Guid("04e27fb5-ea03-4d05-a20b-cc30436bf3e3"),
                        Name = "Mobile",
                        Description = "mobile desc"
                    },
                    new Category {
                        Guid = new Guid("23c29d5a-a2f0-440e-beb3-b7b621ba1628"),
                        Name = "Printer",
                        Description = "printer desc"
                    }
                }
            );
        }
        public List<Category> GetCategories()
        {
            return categories;
        }

        public List<Category> GetCategoryById(Guid id)
        {
            return id == null ? categories : categories.Where(x => x.Guid == id).ToList();
        }
    }
}
