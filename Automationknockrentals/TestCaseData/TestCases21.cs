using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationNUnit
{
    class TestCases21 : IEnumerable
    {
        private List<Dictionary<String, String>> TestCaseList = new List<Dictionary<string, string>>();

        public TestCases21()
        {
            string name = this.GetType().Name;
            TestCaseList.AddRange(Util.ReadTestDataCSVContent(name));
        }

        public IEnumerator GetEnumerator()
        {
            return TestCaseList.GetEnumerator();
        }
    }
}
