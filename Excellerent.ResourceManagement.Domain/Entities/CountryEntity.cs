using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;

namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class CountryEntity : BaseEntity<Country>
    {
        public string Name { get; set; }

        public string Nationality { get; set; }

        public CountryEntity()
        {

        }

        public CountryEntity(Country country) : base(country)
        {
            Guid = country.Guid;
            Name = country.Name;
            CreatedDate = country.CreatedDate;
            CreatedbyUserGuid = country.CreatedbyUserGuid;
            IsActive = country.IsActive;
            IsDeleted = country.IsDeleted;
        }

        public override Country MapToModel()
        {
            Country country = new Country();
            country.Guid = Guid;
            country.Name = Name;
            country.CreatedDate = CreatedDate;
            country.CreatedbyUserGuid = CreatedbyUserGuid;
            country.IsActive = IsActive;
            country.IsDeleted = IsDeleted;

            return country;
        }

        public override Country MapToModel(Country t)
        {
            Country country = t;
            country.Guid = Guid;
            country.Name = Name;
            country.CreatedDate = CreatedDate;
            country.CreatedbyUserGuid = CreatedbyUserGuid;
            country.IsActive = IsActive;
            country.IsDeleted = IsDeleted;

            return country;
        }
    }
}
