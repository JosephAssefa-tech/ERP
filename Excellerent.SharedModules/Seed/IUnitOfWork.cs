using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Excellerent.SharedModules.Seed
{
    public interface IUnitOfWork
    {
        bool IsInTransaction { get; }
        Task SaveChanges();
        Task SaveChanges(SaveOptions saveOptions);
        Task BeginTransaction();
        Task BeginTransaction(IsolationLevel isolationLevel);
        Task RollBackTransaction();
        Task CommitTransaction();
    }
}
