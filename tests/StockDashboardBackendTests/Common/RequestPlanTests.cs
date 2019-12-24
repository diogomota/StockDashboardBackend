using System.Collections.Generic;
using NUnit.Framework;
using StockDashboardBackend.Common;

namespace StockDashboardBackendTests.Common
{
    public class RequestPlanTests
    {
        [Test]
        public void RequestPlan_Initialization_BuildCorrectUrl()
        {
            //Given url and arguments
            string url = "localhost/testEndpoint/";
            Dictionary<string,string> arguments = new Dictionary<string, string>()
            {
                {"argName1","argValue1"},
                {"argName2","argValue2"}
            };
            //When RequestPlan is initialized
            var request = new RequestPlan(url, arguments);
            //Then RequestUrl is built in the expected format
            Assert.AreEqual(request.RequestUrl,"localhost/testEndpoint?argName1=argValue1&argName2=argValue2");
        }
        [Test]
        public void RequestPlan_WithNoArguments_BuildCorrectUrl()
        {
            //Given url and arguments
            string url = "localhost/testEndpoint/";
            //When RequestPlan is initialized
            var request = new RequestPlan(url,null);
            //Then RequestUrl is built in the expected format
            Assert.AreEqual(request.RequestUrl,"localhost/testEndpoint");
        }
    }
}