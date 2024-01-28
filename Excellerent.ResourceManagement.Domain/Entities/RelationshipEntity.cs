using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class RelationshipEntity:BaseEntity<Relationship>
    {
        public string Name { get; set; }

        public override Relationship MapToModel()
        {
            Relationship familyMember = new Relationship();
            this.Name = familyMember.Name;
            return familyMember;
        }

        public override Relationship MapToModel(Relationship t)
        {
            Relationship familyMember = t;
            this.Name = familyMember.Name;
            return familyMember;
        }
    }
}
