using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Automationknockrentals.TestCaseData
{
    class TestCaseDataBase
    {
        //unusedFunctions
        public static IEnumerable<Dictionary<String, String>> PrepareTestCases(string param1)
        {
            string name = param1;
            //if(name.Equals("Billing\\TC_12_VerifyCustomFiltersFunctionalityInInvoiceManagementPageMissingTab"))
            //{
            //    Console.WriteLine("");
            //}            
            return Util.ReadTestDataCSVContent(name);
        }

    }
}
