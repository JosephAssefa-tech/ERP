using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class SubCategoryEntity : BaseEntity<SubCategory>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override SubCategory MapToModel()
        {
            SubCategory s = new SubCategory();
            s.Name = Name;
            s.Description = Description;
            return s;
        }

        public override SubCategory MapToModel(SubCategory t)
        {
            SubCategory s = t;
            s.Name = Name;
            s.Description = Description;
            return s;
        }
    }
}
