using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.EppConfiguration.Presentation.Resource.Dtos
{
    public class ResourceResponseDto
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public dynamic data { get; set; }
    }
}
