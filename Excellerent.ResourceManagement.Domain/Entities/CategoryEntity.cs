using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class CategoryEntity : BaseEntity<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override Category MapToModel()
        {
            Category c = new Category();
            c.Name = Name;
            c.Description = Description;
            return c;
        }

        public override Category MapToModel(Category t)
        {
            Category c = t;
            c.Name = Name;
            c.Description = Description;
            return c;
        }
    }
}
