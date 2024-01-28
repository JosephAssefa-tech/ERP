using Excellerent.ResourceManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace Excellerent.ResourceManagement.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        public List<Category> GetCategories();
        public List<Category> GetCategoryById(Guid id);
    }
}
