using System.Collections.Generic;

namespace AventStack.ExtentReports.ViewDefs
{
    public static class TWBSIcon
    {
        private static readonly Dictionary<Status, string> DictionaryStatus = new Dictionary<Status, string>();

        public static void Override(Status status, string icon)
        {
            DictionaryStatus.Add(status, icon);
        }

        public static string GetIcon(Status status)
        {
            if (DictionaryStatus.ContainsKey(status))
                return DictionaryStatus[status];

            switch (status)
            {
                case Status.Fail:
                    return "times";
                case Status.Fatal:
                    return "times";
                case Status.Error:
                    return "exclamation";
                case Status.Warning:
                    return"warning";
                case Status.Skip:
                    return "long-arrow-right";
                case Status.Pass:
                    return "check";
                case Status.Debug:
                    return "low_priority";
                case Status.Info:
                    return "info";
                default:
                    return "help";
            }
        }
    }
}
