using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System.ComponentModel.DataAnnotations;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class NationalityEntity : BaseEntity<Nationality>
    {


        public string Name { get; set; }

        public override Nationality MapToModel()
        {
            Nationality nationality = new Nationality();
            nationality.Name = Name;

            return nationality;
        }

        public override Nationality MapToModel(Nationality t)
        {
            // dont see why this was neccesary
            return t;
        }
    }
}
