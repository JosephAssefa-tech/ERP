using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;

namespace Excellerent.Timesheet.Domain.Entities
{
    public class ConfigurationEntity : BaseEntity<Configuration>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }

        public ConfigurationEntity() : base()
        {
            Key = string.Empty;
            Value = string.Empty;
            DataType = string.Empty;
        }
        public ConfigurationEntity(string key, string value, string dataType) : base()
        {
            Key = key;
            Value = value;
            DataType = dataType;
        }
        public ConfigurationEntity(Configuration configuration) : base(configuration)
        {
            Key = configuration.Key;
            Value = configuration.Value;
            DataType = configuration.DataType;
        }

        public override Configuration MapToModel() 
        {
            Configuration configuration = new Configuration();

            configuration.Key = Key;
            configuration.Value = Value;
            configuration.DataType = DataType;

            return configuration;
        }
        public override Configuration MapToModel(Configuration config) 
        {
            Configuration configuration = new Configuration();

            configuration.Key = config.Key;
            configuration.Value = config.Value;
            configuration.DataType = config.DataType;

            return configuration;
        }
    }
}
