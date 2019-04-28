using System;
using System.IO;
using System.Reflection;

namespace AventStack.ExtentReports.Utils
{
    internal class ResourceUtils
    {
        public static string GetResource(string folder, string fileName)
        {
            string result;
            string resourceName = folder + "." + fileName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            using (StreamReader sr = new StreamReader(stream ?? throw new InvalidOperationException($"Resource: {resourceName} could not be resolved")))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource?.CopyTo(file);
                }
            }
        }
    }
}
