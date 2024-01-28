using System;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    internal class ApiVersionAttribute : Attribute
    {
        private string v;

        public ApiVersionAttribute(string v)
        {
            this.v = v;
        }
    }
}