using Excellerent.EppConfiguration.Domain.Model;
using Excellerent.SharedModules.Seed;

namespace Excellerent.EppConfiguration.Domain.Entities
{
    public class DepartmentEntity : BaseEntity<Department>
    {
        public string Name { get; set; }

        public DepartmentEntity() { }
        public DepartmentEntity(Department d) : base(d)
        {
            this.Name = d.Name;
        }
        public override Department MapToModel()
        {
            Department d = new Department();
            d.Guid = this.Guid;
            d.Name = this.Name;
            return d;
        }

        public override Department MapToModel(Department t)
        {
            Department d = new Department();
            d.Guid = t.Guid;
            d.Name = t.Name;
            return d;
        }
    }
}
