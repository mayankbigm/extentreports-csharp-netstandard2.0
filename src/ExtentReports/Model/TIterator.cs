using System;
using System.Collections;
using System.Collections.Generic;

namespace AventStack.ExtentReports.Model
{
    [Serializable]
    public class TIterator<T> : IEnumerable<T>
    {
        private readonly List<T> _theList;

        public TIterator(List<T> list)
        {
            _theList = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _theList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
