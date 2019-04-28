using System.Collections.Generic;
using System.Linq;

namespace AventStack.ExtentReports.Configuration
{
    public class ConfigurationManager
    {
        public List<Config> Configuration { get; internal set; } = new List<Config>();
        
        public string GetValue(string k)
        {
            var config = Configuration.Where(x => x.Key.Equals(k)).ToList();

            return config.Any() ? config.First().Value : null;
        }

        public void AddConfig(Config config)
        {
            if (ContainsConfig(config.Key))
                RemoveConfig(config.Key);

            Configuration.Add(config);
        }

        private bool ContainsConfig(string k)
        {
            return Configuration.Count(x => x.Key.Equals(k)) == 1;
        }

        private void RemoveConfig(string k)
        {
            var config = Configuration.First(x => x.Key.Equals(k));
            Configuration.Remove(config);
        }
    }
}
