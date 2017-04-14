using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SpeedTyper.LogicLayer;
using SpeedTyper.DataObjects;
using Microsoft.AspNet.SignalR.Hubs;

namespace SpeedTyper.WebUI.Hubs
{
    [HubName("testHub")]
    public class TestHub : Hub
    {
        ITestManager testManager;
        public TestHub(ITestManager _testManager)
        {
            testManager = _testManager;
        }

        public void StartTest()
        {
            System.Diagnostics.Debug.WriteLine("Start Test");
            TestData testData = testManager.RetrieveRandomTest();
            Clients.Caller.beginTest(testData.TestDataText, testData.DataSource, testData.TestID);
        }

        public void Notify(string msg)
        {
            Clients.All.whisper(msg);
        }
    }
}