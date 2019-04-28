using System;
using System.Collections.Generic;
using System.Linq;

namespace AventStack.ExtentReports.Utils
{
    public static class ListUtil
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            if (enumerable is ICollection<T> collection)
                return collection.Count < 1;

            return !enumerable.Any();
        }

        public static bool Contains(this List<string> list, string keyword, StringComparison comp)
        {
            return list.FindIndex(x => (string.Compare(x.Trim().ToLower(), keyword, comp) == 0)) != -1;
        }
    }
}
