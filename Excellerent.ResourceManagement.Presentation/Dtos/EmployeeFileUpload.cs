using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Dtos
{
    public class EmployeeFileUpload
    {
        public string id { get; set; }
        public IFormFile data { get; set; }

    }
}
