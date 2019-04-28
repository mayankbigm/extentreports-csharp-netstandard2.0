using AventStack.ExtentReports.Configuration;

namespace AventStack.ExtentReports.Reporter
{
    public abstract class ConfigurableReporter : AbstractReporter
    {
        public ConfigurationManager MasterConfig { get; protected internal set; } = new ConfigurationManager();

    }
}
