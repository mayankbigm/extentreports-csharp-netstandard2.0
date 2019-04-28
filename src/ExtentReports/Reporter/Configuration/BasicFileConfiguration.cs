namespace AventStack.ExtentReports.Reporter.Configuration
{
    public class BasicFileConfiguration : BasicConfiguration
    {
        public BasicFileConfiguration(AbstractReporter reporter) : base(reporter)
        {
        }

        public string Encoding
        {
            get => _encoding;
            set
            {
                _encoding = value;
                UserConfigurationMap.Add("encoding", _encoding);
            }
        }

        public string DocumentTitle
        {
            get => _documentTitle;
            set
            {
                _documentTitle = value;
                UserConfigurationMap.Add("documentTitle", _documentTitle);
            }
        }

        private string _encoding;
        private string _documentTitle;
    }
}
