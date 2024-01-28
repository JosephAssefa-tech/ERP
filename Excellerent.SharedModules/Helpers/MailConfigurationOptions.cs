using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.SharedModules.Helpers
{
    public class MailConfigurationOptions
    {
        public string DisplayName { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RequestInfo { get; internal set; }
    }
}
