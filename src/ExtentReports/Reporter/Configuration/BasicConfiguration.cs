using System.Collections.Generic;

namespace AventStack.ExtentReports.Reporter.Configuration
{
    public abstract class BasicConfiguration
    {
        public AbstractReporter Reporter { get; protected internal set; }
        public Dictionary<string, string> UserConfigurationMap => new Dictionary<string, string>();
        private string _reportName;

        public string ReportName
        {
            get => _reportName;
            set
            {
                UserConfigurationMap.Add("reportName", value);
                _reportName = value;
            }
        }

        protected BasicConfiguration(AbstractReporter reporter)
        {
            Reporter = reporter;
        }
    }
}
