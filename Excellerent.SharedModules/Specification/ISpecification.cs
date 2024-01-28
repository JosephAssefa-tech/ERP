using System.Linq;

namespace Excellerent.SharedModules.Specification
{
    public interface ISpecification<TEntity>
    {
        TEntity SatisfyingEntityFrom(IQueryable<TEntity> query);

        IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query);
    }
}
