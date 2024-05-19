using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MusicEvent.Test.IntegrationTest
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public const string PriorityPropertyName = "Priority";

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (TTestCase testCase in testCases)
            {
                int priority = 0;

                foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(PriorityAttribute).AssemblyQualifiedName)))
                {
                    priority = attr.GetNamedArgument<int>(PriorityPropertyName);
                }

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.SelectMany(priority => sortedMethods[priority].OrderByDescending(x => x.DisplayName)))
            {
                yield return list;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue result;

            if (dictionary.TryGetValue(key, out result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
        public int Priority { get; private set; }
    }
}
