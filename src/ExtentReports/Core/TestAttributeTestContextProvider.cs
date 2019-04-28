﻿using AventStack.ExtentReports.Model;

using System.Collections.Generic;
using System.Linq;

namespace AventStack.ExtentReports.Core
{
    public class TestAttributeTestContextProvider<T> where T : TestAttribute
    {
        public TestAttributeTestContextProvider()
        {
            Context = new List<TestAttributeTestContext<T>>();
        }

        public List<TestAttributeTestContext<T>> Context { get; }

        public void AddAttributeContext(T attribute, Test test)
        {
            var context = Context.Where(x => x.Name.Equals(attribute.Name)).ToList();

            if (context.Any())
            {
                if (context.First().TestCollection.All(x => x.TestId != test.TestId))
                {
                    context.First().TestCollection.Add(test);
                }
                context.First().RefreshTestStatusCounts();
            }
            else
            {
                var testAttrTestContext = new TestAttributeTestContext<T>(attribute);
                testAttrTestContext.AddTest(test);
                Context.Add(testAttrTestContext);
            }
        }

        public void RemoveTest(Test test)
        {
            for (int i = Context.Count - 1; i >= 0; i--)
            {
                var context = Context[i];
                TestRemoveService.Remove(context.TestCollection, test);
                if (context.Count == 0)
                {
                    Context.RemoveAt(i);
                }
            }
        }
    }
}
