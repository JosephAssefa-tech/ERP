using Excellerent.APIModularization.Common;

namespace Excellerent.APIModularization.Logging
{
    public interface IBusinessLog
    {
        void LogToDb(string application, BusinessLogModel businessLogModel, UserModel user, string feature, object data, object previousData);
    }
}
