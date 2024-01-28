using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData
{
    public interface ITestDataService
    {
        Task<bool> IsEmptyData();
        Task Add();
    }
}
