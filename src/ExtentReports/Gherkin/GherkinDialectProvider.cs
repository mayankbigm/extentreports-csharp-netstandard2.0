using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AventStack.ExtentReports.Gherkin
{
    internal static class GherkinDialectProvider
    {
        private static readonly Dictionary<string, GherkinKeywords> Dialects;
        public static GherkinKeywords Keywords { get; private set; }
        private static string _dialect;
        public static string DefaultDialect { get; } = "en";
        public static GherkinDialect Dialect { get; private set; }

        static GherkinDialectProvider()
        {
            string json;
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "AventStack.ExtentReports.Resources.Languages.txt";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            Dialects = JsonConvert.DeserializeObject<Dictionary<string, GherkinKeywords>>(json);
        }

        public static string Language
        {
            get
            {
                if (string.IsNullOrEmpty(_dialect))
                    _dialect = DefaultDialect;
                return _dialect;
            }
            set
            {
                _dialect = value;

                if (!Dialects.ContainsKey(_dialect))
                    throw new InvalidGherkinLanguageException(_dialect + " is not a valid Gherkin dialect");

                Keywords = Dialects[_dialect];
                Dialect = new GherkinDialect(_dialect, Keywords);
            }
        }
    }
}
