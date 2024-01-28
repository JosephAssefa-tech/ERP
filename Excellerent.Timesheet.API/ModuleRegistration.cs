using Excellerent.APIModularization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.API
{
    public class ModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
