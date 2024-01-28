using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Excellerent.ReportManagement.Core.Resource
{
    public class NotificationEmailTemplateResourceReader
    {
        private static readonly string resourceFileName = "Excellerent.ReportManagement.Core.Resource.NotificationEmailTemplate";

        public static async Task<string> GetResourceData(string key) 
        {
            ResourceManager resourceManager = new ResourceManager(resourceFileName, Assembly.GetExecutingAssembly());

            string resourceValue = resourceManager.GetString(key);

            return resourceValue;
        }
    }
}
