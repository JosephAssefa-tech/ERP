using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.SharedModules.Interface.Service
{
    public interface ICRUD<T1, T2> where T1 : BaseEntity<T2> where T2 : BaseAuditModel
    {
        Task<ResponseDTO> Add(T1 t);
        Task<ResponseDTO> Update(T1 t);
        Task<ResponseDTO> Delete(T1 t);
        Task<IEnumerable<T2>> GetAll();

    }
}
